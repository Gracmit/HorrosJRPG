using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSkillData", menuName = "Skill/Data/BuffSkillData")]
public class BuffSkillData : SkillData
{
    [SerializeField] private List<StatType> _buffTargets;
    [SerializeField] private int _length;
    [SerializeField] private float _multiplier;
    public List<StatType> Stat => _buffTargets;
    public float Multiplier => _multiplier;
    public int Lenght => _length;
}