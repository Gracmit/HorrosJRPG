using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    [SerializeField] private Item[] _items;
    public void Interact(GameObject player)
    {
        var inventory = FindObjectOfType<Inventory>();
        foreach (var item in _items)
        {
            inventory.PickUpItem(item);
            Debug.Log($"Picked up {item}");
        }
    }
}