using UnityEngine;

public class AttackHandler
{
    private ICombatEntity _attacker;
    private ICombatEntity _target;
    private Skill _skill;
    private bool _attacked;
    public bool Attacked => _attacked;
    public object Target => _target;

    public void SaveAttack(Skill skill)
    {
        _skill = skill;
    }

    public void SaveTarget(ICombatEntity target)
    {
        _target = target;
    }

    public void SaveAttacker(ICombatEntity attacker)
    {
        _attacker = attacker;
    }

    public void Attack()
    {
        var damage = CountDamage();
        bool affected = false;
        if (_skill.StatusEffect.EffectType != EffectType.None)
        {
            affected = EffectWorked();
        }

        if (affected)
        {
            _target.ChangeElement(_skill.StatusEffect.Element);
        }
        _target.TakeDamage(damage);
        Debug.Log($"{_attacker.Data.Name} attacked {_target.Data.Name} with skill {_skill.Name}");
        _target = null;
        _attacker = null;
        _skill = null;
        _attacked = true;
    }

    private bool EffectWorked()
    {
        var number = Random.Range(1, 100);
        return _skill.StatusEffect.Chance >= number;
    }

    private int CountDamage()
    {
        var attackPower = _attacker.Data.Stats.GetValue(_skill.AttackType);
        var targetDefence = _target.Data.Stats.GetValue(_skill.DefenceType);
        var skillPower = _skill.Power;
        var damage = attackPower * skillPower / 2 - targetDefence;
        if (_target.Element == ElementType.None)
            return damage;
        if (_skill.Strength == _target.Element)
            damage *= 2;
        if (_skill.Weakness == _target.Element)
            damage /= 2;
        return damage;
    }
}