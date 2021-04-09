using UnityEngine;

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