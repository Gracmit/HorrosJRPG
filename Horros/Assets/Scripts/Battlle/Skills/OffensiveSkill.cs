#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "OffensiveSkill", menuName = "Skill/OffensiveSkill")]
public class OffensiveSkill : Skill
{
    [SerializeField] private OffensiveSkillData _data;

    public OffensiveSkillData Data => _data;

    public OffensiveSkill(OffensiveSkillData data)
    {
        _data = data;
    }

    
    public override void HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        var damage = CountDamage(attacker, target);
        bool affected = false;
        if (_data.StatusEffect.EffectType != EffectType.None)
        {
            affected = EffectWorked();
        }

        if (affected)
        {
            target.ChangeElement(_data.StatusEffect.Element);
        }

        Debug.Log($"{attacker.Data.Name} attacked {target.Data.Name} with skill {_data.Name}");
        target.TakeDamage(damage);
    }

    private int CountDamage(ICombatEntity attacker, ICombatEntity target)
    {
        var attackPower = attacker.Data.Stats.GetValue(_data.AttackType);
        var targetDefence = target.Data.Stats.GetValue(_data.DefenceType);
        var skillPower = _data.Power;
        var damage = attackPower * skillPower / 2 - targetDefence;
        if (target.Element == ElementType.None)
            return damage;
        if (_data.Strength == target.Element)
            damage *= 2;
        if (_data.Weakness == target.Element)
            damage /= 2;
        return damage;
    }

    private bool EffectWorked()
    {
        var number = Random.Range(1, 100);
        return _data.StatusEffect.Chance >= number;
    }
    
    
#if UNITY_EDITOR
    private void Awake()
    {
        var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
        name = fileName;
    }
#endif
}