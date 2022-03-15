using UnityEngine;

//
public class Consumable : Item
{
    [SerializeField] private ConsumableData _data;

    public ConsumableData ConsumableData => _data;


}