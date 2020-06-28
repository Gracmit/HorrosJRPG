using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();
    
    public int ItemsCount => _items.Count;
    public List<Item> Items => _items;

    public int ItemAmount(Item item)
    {
        Item searchedItem = _items.Find(x => x.name == item.name);
        return searchedItem.Amount;
    }

    public void PickUpItem(Item item)
    {
        Item ogItem = _items.Find(x => x.name == item.name);
        if (ogItem != null)
        {
            ogItem.AddItems(item.Amount);
        }
        else
        {
            _items.Add(item);
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        Item ogItem = _items.Find(x => x.name == item.name);
        if (ogItem == null)
            return;

        bool removedAll = ogItem.SubstractItems(amount);
        if (removedAll)
            _items.Remove(ogItem);
    }

}