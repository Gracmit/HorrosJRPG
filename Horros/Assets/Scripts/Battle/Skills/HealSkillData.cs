using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "HealSkill", menuName = "Skill/Data/HealSkill")]
public class HealSkillData : SkillData
{
    [SerializeField] private int _power;
    [SerializeField] private StatType _statToHeal;
    [SerializeField] private HealingType _type;

    public int Power => _power;
    public HealingType HealingType => _type;
}

public enum HealingType
{
    Constant,
    Percent,
}