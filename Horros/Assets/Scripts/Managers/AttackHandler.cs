using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackHandler : MonoBehaviour
{
    private ICombatEntity _attacker;
    private ICombatEntity _target;
    private Skill _skill;
    private Consumable _item;
    private bool _attackChosen;
    private bool _attacked;

    public bool AttackChosen => _attackChosen;
    public bool Attacked => _attacked;
    public Skill Skill => _skill;

    public void SaveAttack(Skill skill)
    {
        _skill = skill;
        _item = null;
    }

    public void SaveTarget(ICombatEntity target)
    {
        _target = target;
        if(_item != null)
            GameManager.Instance.Inventory.RemoveItem(_item, 1);
        _attackChosen = true;
        Debug.Log(_attacker.Data.Name);
    }

    public void SetAttacker(ICombatEntity attacker)
    {
        _attacker = attacker;
        _attacked = false;
    }

    public void Attack()
    {
        if (_target == null)
            return;
        
        if (!_target.Alive && _skill.GetType() != typeof(ReviveSkill))
            FindNewTarget();
        
        StartCoroutine(HandleAttack());
    }

    public IEnumerator HandleAttack()
    {
        yield return StartCoroutine(_skill.HandleAttack(_attacker, _target));
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
        _item = null;
        _attackChosen = false;
        _attacked = true;
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
        _item = null;
        _attacked = false;
        _attackChosen = false;
    }

    public void ToggleAttacked()
    {
        _attacked = true;
    }

    public void SaveItem(Consumable item)
    {
        _skill = item.Effect;
        _item = item;
    }
}