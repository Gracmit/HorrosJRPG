using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackHandler : MonoBehaviour
{
    private ICombatEntity _attacker;
    private List<ICombatEntity> _targets = new List<ICombatEntity>();
    private Skill _skill;
    private Consumable _item;
    private bool _attackChosen;
    private bool _attacked;

    public bool AttackChosen => _attackChosen;
    public bool Attacked => _attacked;
    public Skill Skill => _skill;
    public List<ICombatEntity> Targets => _targets;

    public void SaveAttack(Skill skill)
    {
        _skill = skill;
        _item = null;
    }

    public void SaveTargets(ICombatEntity target)
    {
        if (_targets == null)
            _targets = new List<ICombatEntity>();
        _targets.Clear();
        _targets.Add(target);
        if(_item != null)
            GameManager.Instance.Inventory.RemoveItem(_item, 1);
        _attackChosen = true;
        Debug.Log(_attacker.Data.Name);
    }
    
    public void SaveTargets(List<ICombatEntity> targettedGroup)
    {
        _targets = targettedGroup;
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
        if (_targets == null)
            return;
        
        if (!TargetsAreAlive() && _skill.GetType() != typeof(ReviveSkill))
            FindNewTarget();
        
        StartCoroutine(HandleAttack());
    }

    private bool TargetsAreAlive()
    {
        var alive = false;
        foreach (var target in _targets)
        {
            if (target.Alive)
            {
                alive = true;
            }
        }

        return alive;
    }

    public IEnumerator HandleAttack()
    {
        BattleUIManager.Instance.EnableAttackText(_skill.Data.Name);
        yield return StartCoroutine(_skill.HandleAttack(_attacker, _targets));
        if (_targets == null) yield break;
        if (_targets[0].GetType() == typeof(PartyMember))
        {
            foreach (var target in _targets)
            {
                BattleUIManager.Instance.UpdateStatusPanel((PartyMember) target);
            }
        }

        if (_attacker.GetType() == typeof(PartyMember))
        {
            BattleUIManager.Instance.UpdateStatusPanel((PartyMember) _attacker);
        }

        yield return new WaitForSeconds(0.5f);
        
        BattleUIManager.Instance.DisableAttackText();
        yield return new WaitForSeconds(0.5f);
        _targets = null;
        _skill = null;
        _item = null;
        _attackChosen = false;
        _attacked = true;
    }

    private void FindNewTarget()
    {
        _targets.Clear();
        if (_targets.GetType() == typeof(PartyMember))
        {
            foreach (var member in BattleManager.Instance.Party.Where(member => member.Alive))
            {
                _targets.Add(member);
                return;
            }
        }
        else
        {
            foreach (var enemy in BattleManager.Instance.Enemies.Where(enemy => enemy.Alive))
            {
                _targets.Add(enemy);
                return;
            }
        }

        _targets = null;
    }

    public void ResetAttack()
    {
        _targets = null;
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