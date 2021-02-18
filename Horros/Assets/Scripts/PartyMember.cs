using System;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "PartyMember", menuName = "CombatEntity/Member")]
[Serializable]
public class PartyMember : ScriptableObject, ICombatEntity
{
    [SerializeField] private string _name;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private Accessory _accessory;
    [SerializeField] private Stats _stats = new Stats();
    [SerializeField] private GameObject _model;
    [SerializeField] private bool _active;
    private bool _attacked;
    private bool _alive;

    public Weapon Weapon => _weapon;
    public Armor Armor => _armor;
    public Accessory Accessory => _accessory;
    public bool Active => _active;

    public GameObject Model => _model;
    public bool Attacked => _attacked;
    public bool Alive => _alive;
    
    public PartyMember()
    {
        _alive = true;
        _weapon = null;
        _armor = null;
        _armor = null;
    }

    public void Equip(Weapon weapon) => _weapon = weapon;

    public void Equip(Armor armor) => _armor = armor;

    public void Equip(Accessory accessory) => _accessory = accessory;

    public void UnEquipWeapon() => _weapon = null;

    public void UnEquipArmor() => _armor = null;

    public void UnEquipAccessory() => _accessory = null;

    public void SetActive(bool active) => _active = active;

    public void TakeDamage()
    {
        Debug.Log("Damage Taken");
        if (_stats.Get(StatType.HP) <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        BattleManager.Instance.RemoveFromTurnQueue(this);
    }
}