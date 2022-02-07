using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[Serializable]
public class PartyMember : ICombatEntity
{
    [SerializeField] private PartyMemberData _data;
    [SerializeField] private bool _active;
    private StatusEffect _statusEffect;
    private Dictionary<StatType, BuffCounter> _activeBuffs = new Dictionary<StatType, BuffCounter>();
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

    public ElementType Element
    {
        get
        {
            if (_statusEffect == null)
            {
                return ElementType.Null;
            }
            return _statusEffect.Element;
        }
    }

    public StatusEffect Effect => _statusEffect ?? new StatusEffect(BattleUIManager.Instance.GetNoneStatusIcon());

    public bool Alive
    {
        get => _alive;
        set => _alive = value;
    }

    public EntityData Data => _data;
    public PartyMemberData PartyMemberData => _data;

    public GameObject CombatAvatar
    {
        get => _combatAvatar;
        set => _combatAvatar = value;
    }

    public PartyMember(PartyMemberData data)
    {
        _alive = true;
        _data = data;
        _combatAvatar = _data.Model;
        _activeBuffs = new Dictionary<StatType, BuffCounter>();
    }

    public void Equip(Weapon weapon) => _data.Weapon = weapon;

    public void Equip(Armor armor) => _data.Armor = armor;

    public void Equip(Accessory accessory) => _data.Accessory = accessory;

    public void UnEquipWeapon() => _data.Weapon = null;

    public void UnEquipArmor() => _data.Armor = null;

    public void UnEquipAccessory() => _data.Accessory = null;

    public void SetActive(bool active) => _active = active;
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
        _alive = false;
        _combatAvatar.transform.position += Vector3.down / 2;
        _attackHandler.ResetAttack();
        BattleManager.Instance.PartyMemberDied();
    }

    public void Revive()
    {
        _combatAvatar.transform.position += Vector3.up / 2;
        BattleManager.Instance.PartyMemberRevived();
        _alive = true;
    }

    public void ChangeElement(StatusEffect effect)
    {
        _statusEffect = effect;
    }

    public void AddBuff(BuffSkillData buff)
    {
        if(_activeBuffs == null)
            _activeBuffs = new Dictionary<StatType, BuffCounter>();
        for(int i = 0; i < buff.Stat.Count; i++)
        {
            if (_activeBuffs.ContainsKey(buff.Stat[i]))
            {
                if (!_activeBuffs[buff.Stat[i]].ModifyBuff(buff))
                    _activeBuffs.Remove(buff.Stat[i]);
            }
            else
            {
                _activeBuffs.Add(buff.Stat[i], new BuffCounter(buff.Multiplier, buff.Lenght));
            }
        }
    }

    public void Attack()
    {
        if (_alive && _attackHandler.Skill != null)
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

    public void Highlight()
    {
        HighlightHealthBarInstantiator.Instance.ShowHealtBar(this);
    }

    public void UnHighlight()
    {
        HighlightHealthBarInstantiator.Instance.HideHealthBar();
    }

    public void SetCameras(CinemachineVirtualCamera chooseCamera)
    {
        _chooseCamera = chooseCamera;
    }

    public int GetStatValue(StatType type)
    {
        var statValue = _data.Stats.GetValue(type);
        if (_activeBuffs.ContainsKey(type))
            return (int) Mathf.Ceil(_activeBuffs[type].Multiplier * statValue);
        
        return statValue;
    }

    public void CheckBuffs()
    {
        if (_activeBuffs == null)
            _activeBuffs = new Dictionary<StatType, BuffCounter>();

        var removableBuffs = new List<StatType>();
        
        foreach (var keyValuePair in _activeBuffs)
        {
            keyValuePair.Value.DecreaseRemainingTime();
            if (keyValuePair.Value.RemainingTime <= 0)
                removableBuffs.Add(keyValuePair.Key);
        }

        foreach (var buff in removableBuffs)
        {
            _activeBuffs.Remove(buff);
        }        
    }
}