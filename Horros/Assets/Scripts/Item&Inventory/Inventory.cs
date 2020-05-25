using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

    public void PickUpItem(Item item)
    {
        _items.Add(item);
    }
}
