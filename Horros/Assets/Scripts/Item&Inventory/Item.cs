using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] protected int _amount;

    public Item(int itemAmount)
    {
        _amount = itemAmount;
    }

    public Item(int itemAmount, ItemData itemData)
    {
        _amount = itemAmount;
        _itemData = itemData;
    }

    protected Item()
    {
    }

    public string Name => _itemData.Name;
    public int Amount => _amount;
    
    public ItemData ItemData => _itemData;
    

    public void AddItems(int addedAmount)
    {
        _amount += addedAmount;
    }

    public bool SubtractItems(int subtractedAmount)
    {
        _amount -= subtractedAmount;
        if (_amount <= 0)
            return true;

        return false;
    }

}