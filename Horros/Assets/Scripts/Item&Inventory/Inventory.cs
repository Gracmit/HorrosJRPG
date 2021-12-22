using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private int _money = 100;

    public event Action MoneyChanged;

    public int ItemsCount => _items.Count;
    public List<Item> Items => _items;

    public int MoneyAmount => _money;

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
            _items.Add(Instantiate(item));
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

    public List<Item> GetConsumables()
    {
        return _items.FindAll(x => x.GetType() == typeof(Consumable));
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke();
    }

    public void SpendMoney(int amount)
    {
        if (_money - amount >= 0)
        {
            _money -= amount;
            MoneyChanged?.Invoke();
        }
        else
        {
            Debug.LogError("Not enough money to buy");
        }
    }
}