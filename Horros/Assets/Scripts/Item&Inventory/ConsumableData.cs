using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class ConsumableData : ItemData
{
    [SerializeField] private Skill _effect;
    [SerializeField] private ConsumableType _type;

    public Skill Effect => _effect;
    public ConsumableType Type => _type;
}