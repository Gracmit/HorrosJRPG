using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _buyingPrice;
    [SerializeField] private int _sellingPrice;
    
    public string Name => _name;
    public int BuyingPrice => _buyingPrice;
    public string Description => _description;

    public void ChangeName(string newName)
    {
        _name = newName;
        
    }
}