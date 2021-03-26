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
        _target.TakeDamage(damage);
        Debug.Log($"{_attacker.Data.Name} attacked {_target.Data.Name} with skill {_skill.Name}");
        _target = null;
        _attacker = null;
        _skill = null;
        _attacked = true;
    }

    private int CountDamage()
    {
        var attackPower = _attacker.Data.Stats.GetValue(_skill.AttackType);
        var targetDefence = _target.Data.Stats.GetValue(_skill.DefenceType);
        var skillPower = _skill.Power;
        return attackPower * skillPower / 2 - targetDefence;
    }
}