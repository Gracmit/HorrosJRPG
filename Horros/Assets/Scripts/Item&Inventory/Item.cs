using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private int amount = 0;
    public int Amount => amount;

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.PickUpItem(this);
        }
    }

    public void AddItems(int addedAmount)
    {
        amount += addedAmount;
    }

    public bool SubstractItems(int substractedAmount)
    {
        amount -= substractedAmount;
        if (amount <= 0)
            return true;

        return false;
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
