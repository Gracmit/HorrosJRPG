using UnityEngine;

[CreateAssetMenu(fileName = "PartyMember", menuName = "CombatEntity/Member")]
public class PartyMemberData : EntityData
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private Accessory _accessory;
    [SerializeField] private Sprite _combatPortrait;
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public Accessory Accessory { get; set; }
    public Sprite Portrait => _combatPortrait;

}


