using System.Collections.Generic;
using UnityEngine;

public class ItemsPanel : MonoBehaviour
{
    private readonly List<GameObject> _buttons = new List<GameObject>();
    private Inventory _inventory;
    private List<Item> _items;
        
    [SerializeField] private GameObject button;
    public double ButtonsCount => _buttons.Count;
    public string ButtonText(int i) => _buttons[i].GetComponent<ItemButton>().ButtonText;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnEnable()
    {
        UpdateItemsUI();
    }


    private void OnDisable()
    {
        ClearItemButtons();
    }

    private void ClearItemButtons()
    {
        for (var i = 0; i < _buttons.Count; i++) Destroy(_buttons[i]);

        _buttons.Clear();
    }


    public void UpdateItemsUI()
    {
        if (_buttons.Count > 0) ClearItemButtons();

        if (_inventory != null)
        {
            _items = _inventory.Items;
            for (var i = 0; i < _items.Count; i++)
            {
                var newButton = Instantiate(button, transform);
                _buttons.Add(newButton);
                newButton.GetComponent<ItemButton>().SetItem(_items[i]);
            }
        }
    }

    public void BindInventory(Inventory inventory)
    {
        _inventory = inventory;
    }
}