using System;
using UnityEngine;


[Serializable]
public class PartyMember : ICombatEntity
{
    [SerializeField] private PartyMemberData _data;
    [SerializeField] private bool _active;
    private bool _attacked;
    private bool _alive;
    private bool _attackChosen;

    public Weapon Weapon => _data.Weapon;
    public Armor Armor => _data.Armor;
    public Accessory Accessory => _data.Accessory;
    public bool Active => _active;

    public GameObject Model => _data.Model;
    public bool AttackChosen => _attackChosen;
    public void ChooseAttack()
    {
        _attackChosen = true;
    }

    public void ResetChosenAttack()
    {
        _attacked = false;
        _attackChosen = false;
    }

    public void Attack()
    {
        _attacked = true;
    }

    public bool Attacked => _attacked;
    public bool Alive => _alive;
    
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

    public void TakeDamage()
    {
        Debug.Log("Damage Taken");
        if (_data.Stats.Get(StatType.HP) <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        BattleManager.Instance.RemoveFromTurnQueue(this);
    }

}