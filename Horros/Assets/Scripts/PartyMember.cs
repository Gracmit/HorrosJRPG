using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private Accessory _accessory;
    private Stats _stats;
}
