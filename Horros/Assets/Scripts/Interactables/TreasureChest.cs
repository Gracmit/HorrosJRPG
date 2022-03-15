using System;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    [SerializeField] private Item[] _items;
    private ItemPopUp _itemPopUp;
    private Inventory _inventory;

    private void Start()
    {
        _itemPopUp = FindObjectOfType<ItemPopUp>();
        _inventory = FindObjectOfType<Inventory>();
    }

    public void Interact(GameObject player)
    {
        _itemPopUp.ShowItems(_items);
        
        foreach (var item in _items)
        {
            _inventory.PickUpItem(item);
            Debug.Log($"Picked up {item.Name}");
        }
    }
}