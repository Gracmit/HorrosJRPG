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
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
    }
}