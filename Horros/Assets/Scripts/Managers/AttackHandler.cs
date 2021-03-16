using UnityEngine;

public class AttackHandler
{
    private ICombatEntity _attacker;
    private ICombatEntity _target;
    private int _temporaryAttack;
    private bool _attacked;
    public bool Attacked => _attacked;
    public object Target => _target;

    public void SaveAttack()
    {
        _temporaryAttack = 10;
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
        _target.TakeDamage();
        Debug.Log($"{_attacker.Data.Name} attacked {_target.Data.Name} with force of {_temporaryAttack} hp");
        _target = null;
        _attacker = null;
        _temporaryAttack = 0;
        _attacked = true;
        
    }
}