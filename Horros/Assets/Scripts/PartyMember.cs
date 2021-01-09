using System;
using UnityEngine;

[Serializable]
public class PartyMember 
{
    private string _name;
    private Weapon _weapon;
    private Armor _armor;
    private Accessory _accessory;
    private Stats _stats = new Stats();

    public Weapon Weapon => _weapon;
    public Armor Armor => _armor;
    public Accessory Accessory => _accessory;

    public PartyMember()
    {
        _weapon = null;
        _armor = null;
        _armor = null;
        
    }
    public void Equip(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void Equip(Armor armor)
    {
        _armor = armor;
    }
    
    public void Equip(Accessory accessory)
    {
        _accessory = accessory;
    }

    public void UnEquip(Weapon weapon)
    {
        _weapon = null;
    }
    
    public void UnEquip(Armor armor)
    {
        _armor = null;
    }
    
    public void UnEquip(Accessory accessory)
    {
        _accessory = null;
    }
}


