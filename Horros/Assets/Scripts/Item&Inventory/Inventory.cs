using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

    
    public int ItemsCount => _items.Count;
    public List<Item> Items => _items;

    public int ItemAmount(Item item)
    {
        Item searchedItem = _items.Find(x => x.Name == item.Name);
        return searchedItem.Amount;
    }

    
    public void PickUpItem(Item item)
    {
        Item ogItem = _items.Find(x => x.Name == item.Name);
        if (ogItem != null)
        {
            ogItem.AddItems(item.Amount);
        }
        else
        {
            _items.Add(ScriptableObject.CreateInstance<Item>());
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        Item ogItem = _items.Find(x => x.Name == item.Name);
        if (ogItem == null)
            return;

        bool removedAll = ogItem.SubtractItems(amount);
        if (removedAll)
            _items.Remove(ogItem);
    }

}