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
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            Item item = new Item(0, itemData);
            itemData.ChangeName("item1");
            item.AddItems(1);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void removes_item_from_inventory()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            Item item = new Item(0, itemData);
            itemData.ChangeName("item1");
            ItemData itemData2 = ScriptableObject.CreateInstance<ItemData>();
            Item item2 = new Item(0, itemData2);
            item2.ItemData.ChangeName("item2");  
            item2.AddItems(1);
            item2.AddItems(1);
            inventory.PickUpItem(item);
            inventory.PickUpItem(item2);
            inventory.RemoveItem(item, 1);
            
            Assert.AreEqual(1, inventory.ItemsCount);
        }
        
        [Test]
        public void adds_multiple_items_to_inventory()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            Item item = new Item(0, itemData);
            itemData.ChangeName("item1");
            item.AddItems(4);
            inventory.PickUpItem(item);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            
            Assert.AreEqual(4, inventory.ItemAmount(itemData));
        }
        
        [Test]
        public void removes_correct_amount_of_items()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            Item item = new Item(0, itemData);
            itemData.ChangeName("item1");
            item.AddItems(5);
            inventory.PickUpItem(item);
            inventory.RemoveItem(item, 3);
            
            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(2, inventory.ItemAmount(itemData));
        }
        
        [Test]
        public void add_already_existing_items()
        {
            var inventory = new GameObject("Inventory").AddComponent<Inventory>();
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
            Item item = new Item(0, itemData);
            itemData.ChangeName("item1");
            item.AddItems(3);
            ItemData itemData2 = ScriptableObject.CreateInstance<ItemData>();
            Item item2 = new Item(0, itemData2);
            itemData2.ChangeName("item1");
            inventory.PickUpItem(item);
            item2.AddItems(4);
            inventory.PickUpItem(item2);

            Assert.AreEqual(1, inventory.ItemsCount);
            Assert.AreEqual(7, inventory.ItemAmount(itemData));
        }
    }
}