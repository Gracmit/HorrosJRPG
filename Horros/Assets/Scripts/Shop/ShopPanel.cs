﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private ShopButton _shopButton;
    [SerializeField] private GameObject _list;
    [SerializeField] private ShopItemInfo _infoPanel;

    private static ShopPanel _instance;
    private Shop[] _shops;
    private List<ShopButton> _buttons = new List<ShopButton>();
    private Inventory _inventory;


    public static ShopPanel Instance => _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
        _shops = FindObjectsOfType<Shop>();
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnEnable()
    {
        _inventory.MoneyChanged += UpdateButtons;
        foreach (var shop in _shops)
        {
            shop.ShopOpened += UpdateShopUI;
        }
    }

    private void OnDisable()
    {
        _inventory.MoneyChanged -= UpdateButtons;
        foreach (var shop in _shops)
        {
            shop.ShopOpened -= UpdateShopUI;
        }
    }

    private void UpdateButtons()
    {
        foreach (var button in _buttons)
        {
            if (button.Item.BuyingPrice > _inventory.MoneyAmount)
            {
                button.GetComponent<Button>().interactable = false;
            }
            else
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void UpdateShopUI(List<Item> items)
    {
        if (_buttons.Count > 0)
        {
            for (int i = _buttons.Count - 1; i >= 0; i--)
            {
                var button = _buttons[i];
                _buttons.Remove(button);
                Destroy(button.gameObject);
            }
        }
        
        foreach (var item in items)
        {
            CreateButton(item);
            UpdateButtons();
        }
    }

    private void CreateButton(Item item)
    {
        var button = Instantiate(_shopButton, _list.transform);
        _buttons.Add(button);

        var shopButton = button.GetComponent<ShopButton>();
        shopButton.SetItem(item);
    }

    public void ShowInfo(Item item)
    {
        _infoPanel.SetText(item);
    }
}