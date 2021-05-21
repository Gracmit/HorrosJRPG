using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[Serializable]
public class PartyMember : ICombatEntity
{
    [SerializeField] private PartyMemberData _data;
    [SerializeField] private bool _active;
    private ElementType _element = ElementType.None;
    private Dictionary<StatType, BuffCounter> _activeBuffs;
    private bool _alive = true;
    private GameObject _combatAvatar;
    private AttackHandler _attackHandler;
    private CinemachineVirtualCamera _chooseCamera;

    public Weapon Weapon => _data.Weapon;
    public Armor Armor => _data.Armor;
    public Accessory Accessory => _data.Accessory;
    public bool Active => _active;

    public GameObject Model => _data.Model;
    public AttackHandler AttackHandler => _attackHandler;

    public CinemachineVirtualCamera ChooseCamera => _chooseCamera;

    public bool Alive
    {
        get => _alive;
        set => _alive = value;
    }

    public EntityData Data => _data;

    public GameObject CombatAvatar
    {
        get => _combatAvatar;
        set => _combatAvatar = value;
    }

    public PartyMember(PartyMemberData data)
    {
        _alive = true;
        _data = data;
    }

    public void Equip(Weapon weapon) => _data.Weapon = weapon;

    public void Equip(Armor armor) => _data.Armor = armor;

    public void Equip(Accessory accessory) => _data.Accessory = accessory;

    public void UnEquipWeapon() => _data.Weapon = null;

    public void UnEquipArmor() => _data.Armor = null;

    public void UnEquipAccessory() => _data.Accessory = null;

    public void SetActive(bool active) => _active = active;
    public ElementType Element => _element;
    public bool Attacked => _attackHandler.Attacked;

    public void TakeDamage(int damage)
    {
        _data.Stats.Subtract(StatType.HP, damage);
        Debug.Log($"{_data.Name} took {damage} damage. {_data.Stats.GetValue(StatType.HP)} HP remaining");
        if (_data.Stats.GetValue(StatType.HP) <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _combatAvatar.SetActive(false);
        _attackHandler.ResetAttack();
        BattleManager.Instance.PartyMemberDied();
        _alive = false;
    }

    public void ChangeElement(ElementType element)
    {
        _element = element;
    }

    public void AddBuff(BuffSkillData buff)
    {
        if (_activeBuffs.ContainsKey(buff.Stat))
        {
            if (!_activeBuffs[buff.Stat].ModifyBuff(buff))
                _activeBuffs.Remove(buff.Stat);
        }
        else
        {
            _activeBuffs.Add(buff.Stat, new BuffCounter(buff.Multiplier, buff.Lenght));
        }
    }

    public void Attack()
    {
        if (_alive)
            _attackHandler.Attack();
        else
            _attackHandler.ToggleAttacked();
    }

    public void FullHeal()
    {
        _data.Stats.FullHeal();
    }

    public void SetAttackHandler()
    {
        _combatAvatar.AddComponent<AttackHandler>();
        _attackHandler = _combatAvatar.GetComponent<AttackHandler>();
        _attackHandler.SetAttacker(this);
    }

    public void ResetAttack()
    {
        _attackHandler.ResetAttack();
    }

    public void SetCameras(CinemachineVirtualCamera chooseCamera)
    {
        _chooseCamera = chooseCamera;
    }
}

public class BuffCounter
{
    private float _multiplier;
    private int _timeRemaining;

    public BuffCounter(float multiplier, int length)
    {
        _multiplier = multiplier;
        _timeRemaining = length;
    }

    public bool ModifyBuff(BuffSkillData buff)
    {
        if (_multiplier == buff.Multiplier)
        {
            _timeRemaining += buff.Lenght;
            return true;
        }

        return false;
    }
}