using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ReviveSkill", menuName = "Skill/ReviveSkill")]
public class ReviveSkill : Skill
{
    [SerializeField] private HealSkillData _data;

    public override SkillData Data => _data;

    public ReviveSkill(HealSkillData data)
    {
        _data = data;
    }

    public override IEnumerator HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        SubtractMP(attacker);
        var amount = 0;
        if (target.Alive)
            amount = 0;
        else if(target.GetType() == typeof(PartyMember))
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

        attacker.CombatAvatar.transform.position += Vector3.up / 2;
        target.CombatAvatar.transform.position += Vector3.down / 2;
        DamagePopUpInstantiator.Instance.InstantiatePopUp(target, amount);
        target.Data.Stats.Replenish(StatType.HP, amount);
        yield return new WaitForSeconds(2f);
        attacker.CombatAvatar.transform.position += Vector3.down / 2;
        target.CombatAvatar.transform.position += Vector3.up / 2;
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