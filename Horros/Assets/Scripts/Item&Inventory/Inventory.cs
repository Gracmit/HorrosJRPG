using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();
    public int ItemsCount => _items.Count;

    public void PickUpItem(Item item, int amount)
    {
        Item ogItem = _items.Find(x => x.name == item.name);
        if (ogItem != null)
        {
            ogItem.AddItems(amount);
        }
        else
        {
            _items.Add(item);
            item.AddItems(amount);
        }
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }
}