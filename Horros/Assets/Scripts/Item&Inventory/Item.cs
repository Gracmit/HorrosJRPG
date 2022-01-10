using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _amount;
    [SerializeField] private int _buyingPrice;
    [SerializeField] private int _sellingPrice;

    public int Amount => _amount;
    public string Name => _name;
    public int BuyingPrice => _buyingPrice;
    public string Description => _description;

    public void AddItems(int addedAmount)
    {
        _amount += addedAmount;
    }

    public bool SubtractItems(int subtractedAmount)
    {
        _amount -= subtractedAmount;
        if (_amount <= 0)
            return true;

        return false;
    }

    public void ChangeName(string newName)
    {
        _name = newName;
        
    }
}