using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private int amount = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.PickUpItem(this, 1);
        }
    }

    public void AddItems(int addedAmount)
    {
        amount += addedAmount;
    }
}


public class Equipment : Item
{
    [SerializeField] private EquipmentType equipmentType;
}

public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory
}
