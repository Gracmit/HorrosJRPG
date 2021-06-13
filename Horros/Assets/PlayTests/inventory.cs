using NUnit.Framework;
using UnityEngine;

namespace Player
{
    public class inventory
    {
        [Test]
        public void inventory_without_added_items_is_empty()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            
            Assert.AreEqual(0, inventory.ItemsCount);
        }
        
        [Test]
        public void adds_item_to_inventory()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = ScriptableObject.CreateInstance<Item>();
            item.ChangeName("item1");
            item.AddItems(1);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void removes_item_from_inventory()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item1 = ScriptableObject.CreateInstance<Item>();
            item1.ChangeName("item1");
            Item item2 = ScriptableObject.CreateInstance<Item>();
            item2.ChangeName("item2");
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
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = ScriptableObject.CreateInstance<Item>();
            item.ChangeName("item1");
            item.AddItems(4);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            
            Assert.AreEqual(4, inventory.ItemAmount(item));
        }
        
        [Test]
        public void removes_correct_amount_of_items()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = ScriptableObject.CreateInstance<Item>();
            item.ChangeName("item1");
            item.AddItems(5);
            inventory.PickUpItem(item);
            inventory.RemoveItem(item, 3);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(2, inventory.ItemAmount(item));
        }
        
        [Test]
        public void add_already_existing_items()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            Item item = ScriptableObject.CreateInstance<Item>();
            item.ChangeName("item1");
            item.AddItems(3);
            Item item2 = ScriptableObject.CreateInstance<Item>();
            item2.ChangeName("item1");
            inventory.PickUpItem(item);
            item2.AddItems(4);
            inventory.PickUpItem(item2);

            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(7, inventory.ItemAmount(item));
        }
    }
}