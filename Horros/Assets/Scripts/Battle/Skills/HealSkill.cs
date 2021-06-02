using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HealSkill", menuName = "Skill/HealSkill")]
public class HealSkill : Skill
{
    [SerializeField] private HealSkillData _data;

    public override SkillData Data => _data;

    public HealSkill(HealSkillData data)
    {
        _data = data;
    }

    public override IEnumerator HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        SubtractMP(attacker);
        var amount = _data.Power;
        if (_data.HealingType == HealingType.Constant)
        {
            amount = _data.Power;
        }
        else
        {
            amount = CountHealAmount(target);
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