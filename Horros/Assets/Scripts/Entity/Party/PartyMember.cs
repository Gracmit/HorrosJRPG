using System;
using UnityEngine;


[Serializable]
public class PartyMember : ICombatEntity
{
    [SerializeField] private PartyMemberData _data;
    [SerializeField] private bool _active;
    private bool _alive = true;
    private GameObject _combatAvatar;

    public Weapon Weapon => _data.Weapon;
    public Armor Armor => _data.Armor;
    public Accessory Accessory => _data.Accessory;
    public bool Active => _active;

    public GameObject Model => _data.Model;

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

    public void TakeDamage()
    {
        _data.Stats.Remove(StatType.HP, 70);
        Debug.Log($"{_data.Name} took 10 damage. {_data.Stats.GetValue(StatType.HP)} HP remaining");
        if (_data.Stats.GetValue(StatType.HP) <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        BattleManager.Instance.RemoveFromTurnQueue(this);
        _combatAvatar.SetActive(false);
    }
    
    public void FullHeal()
    {
        _data.Stats.FullHeal();
    }
}