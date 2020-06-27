﻿using NUnit.Framework;
using UnityEngine;

namespace Player
{
    public class inventory
    {
        [Test]
        public void inventory_without_added_items_is_empty()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            
            Assert.AreEqual(0, inventory.ItemsCount);
        }
        
        [Test]
        public void adds_item_to_inventory()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item").AddComponent<Item>();
            item.AddItems(1);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void removes_item_from_inventory()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item1 = new GameObject("Item1").AddComponent<Item>();
            Item item2 = new GameObject("Item2").AddComponent<Item>();
            item1.AddItems(1);
            item2.AddItems(1);
            inventory.PickUpItem(item1);
            inventory.PickUpItem(item2);
            inventory.RemoveItem(item1, 1);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void adds_multiple_items_to_inventory()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item").AddComponent<Item>();
            item.AddItems(4);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            
            Assert.AreEqual(4, inventory.ItemAmount(item));
        }
        
        [Test]
        public void removes_correct_amount_of_items()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item1").AddComponent<Item>();
            item.AddItems(5);
            inventory.PickUpItem(item);
            inventory.RemoveItem(item, 3);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(2, inventory.ItemAmount(item));
        }
        
        [Test]
        public void add_already_existing_items()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = new GameObject("Item1").AddComponent<Item>();
            Item item2 = new GameObject("Item1").AddComponent<Item>();
            item.AddItems(3);
            inventory.PickUpItem(item);
            item2.AddItems(4);
            inventory.PickUpItem(item2);

            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(7, inventory.ItemAmount(item));
        }
    }
}