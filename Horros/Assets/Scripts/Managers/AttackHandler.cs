using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackHandler
{
    private ICombatEntity _attacker;
    private ICombatEntity _target;
    private Skill _skill;
    private bool _attackChosen;
    
    public bool AttackChosen => _attackChosen;

    public void SaveAttack(Skill skill)
    {
        _skill = skill;
    }

    public void SaveTarget(ICombatEntity target)
    {
        _target = target;
        _attackChosen = true;
        Debug.Log(_attacker.Data.Name);
    }

    public void SetAttacker(ICombatEntity attacker)
    {
        _attacker = attacker;
    }

    public void Attack()
    {
        if (_target == null)
            return;

        if (!_target.Alive)
            FindNewTarget();
            
        _skill.HandleAttack(_attacker, _target);
        if (_target.GetType() == typeof(PartyMember))
        {
            BattleUIManager.Instance.UpdateStatusPanel((PartyMember)_target);
        }

        if (_attacker.GetType() == typeof(PartyMember))
        {
            BattleUIManager.Instance.UpdateStatusPanel((PartyMember)_attacker);
        }
        
        _target = null;
        _skill = null;
        _attackChosen = false;
    }

    private void FindNewTarget()
    {
        if (_target.GetType() == typeof(PartyMember))
        {
            foreach (var member in BattleManager.Instance.Party.Where(member => member.Alive))
            {
                _target = member;
                return;
            }
        }
        else
        {
            foreach (var enemy in BattleManager.Instance.Enemies.Where(enemy => enemy.Alive))
            {
                _target = enemy;
                return;
            }
        }

        _target = null;
    }

    public void ResetAttack()
    {
        _target = null;
        _skill = null;
    }
}