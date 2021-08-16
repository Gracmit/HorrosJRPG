using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HealSkill", menuName = "Skill/HealSkill")]
public class HealSkill : Skill
{
    [SerializeField] private HealSkillData _data;
    
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int TakeDamage = Animator.StringToHash("Take damage");

    public override SkillData Data => _data;

    public HealSkill(HealSkillData data)
    {
        _data = data;
    }

    public override IEnumerator HandleAttack(ICombatEntity attacker, List<ICombatEntity> targets)
    {
        SubtractMP(attacker);
        
        var animator = attacker.CombatAvatar.GetComponent<Animator>();
        animator.SetTrigger(Attack1);

        foreach (var target in targets)
        {
            var amount = _data.Power;
            if (_data.HealingType == HealingType.Constant)
            {
                amount = _data.Power;
            }
            else
            {
                amount = CountHealAmount(target);
            }

            var animator1 = target.CombatAvatar.GetComponent<Animator>();
            animator1.SetTrigger(TakeDamage);
            
            DamagePopUpInstantiator.Instance.InstantiatePopUp(target, amount);
            target.Data.Stats.Replenish(StatType.HP, amount);
        }

        yield return new WaitForSeconds(2f);
    }

    private int CountHealAmount(ICombatEntity target)
    {
        var maxHP = target.Data.Stats.GetValue(StatType.MaxHP);
        return maxHP / 100 * _data.Power;
    }

    private void SubtractMP(ICombatEntity attacker)
    {
        attacker.Data.Stats.Subtract(StatType.MP, Data.MpCost);
    }


#if UNITY_EDITOR
    private void Awake()
    {
        var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this));
        name = fileName;
    }
#endif
}