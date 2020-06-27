using NUnit.Framework;
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
            inventory.PickUpItem(item, 1);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void removes_item_from_inventory()
        {
            Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item1 = new GameObject("Item1").AddComponent<Item>();
            Item item2 = new GameObject("Item2").AddComponent<Item>();
            inventory.PickUpItem(item1, 1);
            inventory.PickUpItem(item2, 1);
            inventory.RemoveItem(item1);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
    }
}