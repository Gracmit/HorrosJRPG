using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : MonoBehaviour
{
    [SerializeField] private GameObject button;

    private List<Item> _items;
    private List<GameObject> _buttons = new List<GameObject>();
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnEnable()
    {
        _items = _inventory.Items;
        for (int i = 0; i < _items.Count; i++)
        {
            GameObject newButton = Instantiate(button, transform);
            _buttons.Add(newButton);
            newButton.GetComponent<ItemButton>().SetItem(_items[i]);
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            Destroy(_buttons[i]);
        }

        _buttons.Clear();
    }

    public void BindInventory(Inventory inventory)
    {
        _inventory = inventory;
    }
}