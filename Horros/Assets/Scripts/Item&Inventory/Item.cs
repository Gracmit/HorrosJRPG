using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private int amount = 0;

    public int Amount => amount;

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.PickUpItem(this);
        }
    }

    public void AddItems(int addedAmount)
    {
        amount += addedAmount;
    }

    public bool SubtractItems(int subtractedAmount)
    {
        amount -= subtractedAmount;
        if (amount <= 0)
            return true;

        return false;
    }
}