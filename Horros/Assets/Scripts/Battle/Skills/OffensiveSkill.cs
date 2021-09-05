#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "OffensiveSkill", menuName = "Skill/OffensiveSkill")]
public class OffensiveSkill : Skill
{
    [SerializeField] private OffensiveSkillData _data;
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int TakeDamage = Animator.StringToHash("Take damage");

    public override SkillData Data => _data;
    public OffensiveSkillData OffensiveData => _data;

    public OffensiveSkill(OffensiveSkillData data)
    {
        _data = data;
    }


    public override IEnumerator HandleAttack(ICombatEntity attacker, List<ICombatEntity> targets)
    {
        SubtractMP(attacker);

        var animator = attacker.CombatAvatar.GetComponent<Animator>();
        animator.SetTrigger(Attack1);
        yield return new WaitForSecondsRealtime(0.5f);

        foreach (var target in targets)
        {
            var damage = CountDamage(attacker, target);

            var affected = false;
            if (_data.StatusEffect.EffectType != EffectType.None)
                affected = EffectWorked();

            if (affected)
                target.ChangeElement(_data.StatusEffect);

            var animator1 = target.CombatAvatar.GetComponent<Animator>();
            animator1.SetTrigger(TakeDamage);

            HighlightHealthBarInstantiator.Instance.InstantiatePopUpHealthBar(target, damage);
            DamagePopUpInstantiator.Instance.InstantiatePopUp(target, damage);
            target.TakeDamage(damage);
            Debug.Log($"{attacker.Data.Name} attacked {target.Data.Name} with skill {_data.Name}");
        }
        
        yield return new WaitForSeconds(0.1f);
    }


    private void SubtractMP(ICombatEntity attacker)
    {
        attacker.Data.Stats.Subtract(StatType.MP, Data.MpCost);
    }


    private int CountDamage(ICombatEntity attacker, ICombatEntity target)
    {
        var attackPower = attacker.GetStatValue(_data.AttackType);
        var targetDefence = target.GetStatValue(_data.DefenceType);
        var skillPower = _data.Power;
        var damage = attackPower * skillPower / 2 - targetDefence;
        if (damage <= 0)
            damage = 1;
        
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