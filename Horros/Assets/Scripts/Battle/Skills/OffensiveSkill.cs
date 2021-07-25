#if UNITY_EDITOR
    using System.IO;
    using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "OffensiveSkill", menuName = "Skill/OffensiveSkill")]
public class OffensiveSkill : Skill
{
    [SerializeField] private OffensiveSkillData _data;
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int Property = Animator.StringToHash("Take damage");

    public override SkillData Data => _data;
    public OffensiveSkillData OffensiveData => _data;

    public OffensiveSkill(OffensiveSkillData data)
    {
        _data = data;
    }

    
    public override IEnumerator HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        
        var damage = CountDamage(attacker, target);
        SubtractMP(attacker);
        var affected = false;
        if (_data.StatusEffect.EffectType != EffectType.None)
        {
            affected = EffectWorked();
        }

        if (affected)
        {
            target.ChangeElement(_data.StatusEffect.Element);
        }

        var animator = attacker.CombatAvatar.GetComponent<Animator>();
        animator.SetTrigger(Attack1);
        yield return new WaitForSecondsRealtime(0.5f);
        var animator1 = target.CombatAvatar.GetComponent<Animator>();
        animator1.SetTrigger(Property);
        //attacker.CombatAvatar.transform.position += Vector3.up / 2;
        //target.CombatAvatar.transform.position += Vector3.down / 2;
        DamagePopUpInstantiator.Instance.InstantiatePopUp(target, damage);
        yield return new WaitForSeconds(2f);
        //attacker.CombatAvatar.transform.position += Vector3.down / 2;
        //target.CombatAvatar.transform.position += Vector3.up / 2;
        
        Debug.Log($"{attacker.Data.Name} attacked {target.Data.Name} with skill {_data.Name}");
        target.TakeDamage(damage);
    }

    
    private void SubtractMP(ICombatEntity attacker)
    {
        attacker.Data.Stats.Subtract(StatType.MP, Data.MpCost);
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