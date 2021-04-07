using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "OffensiveSkill", menuName = "Skill/Data/OffensiveSkill")]
public class OffensiveSkillData : SkillData
{
    [SerializeField] private int _power;
    [SerializeField] private ElementType _element;
    [SerializeField] private ElementType _weakness;
    [SerializeField] private ElementType _strength;
    [SerializeField] private StatType _attackType;
    [SerializeField] private StatType _defenceType;
    [SerializeField] private StatusEffect _statusEffect;

    public int Power => _power;
    public StatType AttackType => _attackType;
    public StatType DefenceType => _defenceType;
    public StatusEffect StatusEffect => _statusEffect;
    public ElementType Strength => _strength;
    public ElementType Weakness => _weakness;
}

[Serializable]
public class StatusEffect
{
    [SerializeField] private EffectType _type;
    [SerializeField] private ElementType _effectElement;
    [SerializeField] private int _chance;
    public EffectType EffectType => _type;
    public int Chance => _chance;
    public ElementType Element => _effectElement;
}

public enum EffectType
{
    None,
    Burn,
    Freeze,
}

public enum ElementType
{
    None,
    Melee,
    Ranged,
    Fire,
    Ice,
}

[Serializable]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;

    public string Name => _name;
}


[CreateAssetMenu(fileName = "BuffSkillData", menuName = "Skill/Data/BuffSkillData")]
public class BuffSkillData : SkillData
{
    [SerializeField] private StatType _buffTarget;
    [SerializeField] private int _length;
    [SerializeField] private float _multiplier;
    public StatType Stat => _buffTarget;
    public float Multiplier => _multiplier;
    public int Lenght => _length;
}