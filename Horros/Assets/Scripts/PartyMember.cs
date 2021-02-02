using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyMember", menuName = "Party/Member")]
[Serializable]
public class PartyMember : ScriptableObject
{
    [SerializeField]private string _name;
    [SerializeField]private Weapon _weapon;
    [SerializeField]private Armor _armor;
    [SerializeField]private Accessory _accessory;
    [SerializeField]private Stats _stats = new Stats();
    [SerializeField] private EntityStatus _memberStatus;

    public Weapon Weapon => _weapon;
    public Armor Armor => _armor;
    public Accessory Accessory => _accessory;
    public EntityStatus MemberStatus => _memberStatus;

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

    public void UnEquipWeapon()
    {
        _weapon = null;
    }
    
    public void UnEquipArmor()
    {
        _armor = null;
    }
    
    public void UnEquipAccessory()
    {
        _accessory = null;
    }
}


