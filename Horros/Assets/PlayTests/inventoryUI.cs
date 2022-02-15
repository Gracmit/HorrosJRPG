using System.Collections;
using NSubstitute;
using NUnit.Framework;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class inventoryUI
{
    [UnityTest]
    public IEnumerator ui_is_empty_when_inventory_is_empty()
    {
        
        yield return Helpers.LoadUIScene();
        Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
        ItemsPanel itemsPanel = GameObject.FindObjectOfType<ItemsPanel>();

        
        itemsPanel.BindInventory(inventory);
        itemsPanel.UpdateItemsUI();
        
        Assert.AreEqual(0, itemsPanel.ButtonsCount);
    }

    
    [UnityTest]
    public IEnumerator ui_has_as_many_buttons_as_inventory_has_items()
    {
        yield return Helpers.LoadUIScene();
        Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
        
        ItemsPanel itemsPanel = GameObject.FindObjectOfType<ItemsPanel>();
        
        itemsPanel.BindInventory(inventory);

        ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
        Item item = new Item(0, itemData);
        item.ItemData.ChangeName("item1");
        item.AddItems(1);
        inventory.PickUpItem(item);
        
        itemsPanel.UpdateItemsUI();
        
        Assert.AreEqual(1, itemsPanel.ButtonsCount);
        yield return null;
    }
    
    
    [UnityTest]
    public IEnumerator ui_has_shows_correct_amount_of_items()
    {
        yield return Helpers.LoadUIScene();
        Inventory inventory = new GameObject("Inventory").AddComponent<Inventory>();
        
        ItemsPanel itemsPanel = GameObject.FindObjectOfType<ItemsPanel>();
        
        itemsPanel.BindInventory(inventory);
        
        ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
        Item item = new Item(0, itemData);
        itemData.ChangeName("item1");
        item.AddItems(2);
        inventory.PickUpItem(item);
        
        ItemData itemData2 = ScriptableObject.CreateInstance<ItemData>();
        Item item2 = new Item(0, itemData2);
        itemData2.ChangeName("item2");
        item2.AddItems(5);
        inventory.PickUpItem(item2);
        
        itemsPanel.UpdateItemsUI();
        
        Assert.AreEqual("item1 2", itemsPanel.ButtonText(0));
        Assert.AreEqual("item2 5", itemsPanel.ButtonText(1));
        yield return null;
    }

    private ItemsPanel GetItemsPanel()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<ItemsPanel>("Assets/Prefabs/UI/Items Panel.prefab");
        return prefab;
    }
}
