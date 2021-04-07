
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
        _skill.HandleAttack(_attacker, _target);
        
        _target = null;
        _attacker = null;
        _skill = null;
        _attacked = true;
    }

}