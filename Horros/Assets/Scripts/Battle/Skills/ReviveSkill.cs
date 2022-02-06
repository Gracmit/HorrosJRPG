using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ReviveSkill", menuName = "Skill/ReviveSkill")]
public class ReviveSkill : Skill
{
    [SerializeField] private HealSkillData _data;
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int TakeDamage = Animator.StringToHash("Take damage");

    public override SkillData Data => _data;

    public ReviveSkill(HealSkillData data)
    {
        _data = data;
    }

    public override IEnumerator HandleAttack(ICombatEntity attacker, List<ICombatEntity> targets)
    {
        SubtractMP(attacker);

        var animator = attacker.CombatAvatar.GetComponent<Animator>();
        animator.SetTrigger(Attack1);

        yield return new WaitForSeconds(0.5f);

        foreach (var target in targets)
        {
            var amount = 0;
            if (target.Alive)
                amount = 0;
            else if (target.GetType() == typeof(PartyMember))
            {
                if (_data.HealingType == HealingType.Constant)
                {
                    var member = (PartyMember) target;
                    member.Revive();
                    amount = _data.Power;
                }
                else
                {
                    var member = (PartyMember) target;
                    member.Revive();
                    amount = CountHealAmount(target);
                }
            }

            Instantiate(_data.Effect, target.CombatAvatar.GetComponentInChildren<FindTransform>().transform);

            DamagePopUpInstantiator.Instance.InstantiatePopUp(target, amount);
            target.Data.Stats.Replenish(StatType.HP, amount);
        }

        yield return new WaitForSeconds(1f);
    }

    private int CountHealAmount(ICombatEntity target)
    {
        var maxHP = target.Data.Stats.GetValue(StatType.MaxHP);
        var result = maxHP / 100f * _data.Power;
        return Mathf.FloorToInt(result);
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