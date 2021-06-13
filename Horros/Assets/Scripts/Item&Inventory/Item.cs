using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _amount;

    public int Amount => _amount;
    public string Name => _name;

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