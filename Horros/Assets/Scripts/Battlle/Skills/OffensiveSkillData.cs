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