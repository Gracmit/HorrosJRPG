using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _power;
    [SerializeField] private ElementType _element;
    [SerializeField] private ElementType _weakness;
    [SerializeField] private ElementType _strength;
    [SerializeField] private StatType _attackType;
    [SerializeField] private StatType _defenceType;

    public string Name => _name;
    public int Power => _power;
    public StatType AttackType => _attackType;
    public StatType DefenceType => _defenceType;
}

public enum ElementType
{
    None,
    Melee,
    Ranged,
    Fire,
    Ice,
}

