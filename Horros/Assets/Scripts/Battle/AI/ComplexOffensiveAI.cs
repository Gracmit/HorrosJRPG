using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "CombatAI/ComplexOffensiveAI", fileName = "ComplexOffensiveAI")]
public class ComplexOffensiveAI : CombatAI
{
    private ICombatEntity _target;
    private Skill _attack;

    private PartyMember _weakTarget;
    private Skill _effectiveSkill;
    private List<Skill> _skills;
    private List<PartyMember> _party;
    private List<CombatEnemy> _enemies;

    public override Skill GetSkill() => _attack;

    public override ICombatEntity GetTarget() => _target;

    public override void ChooseAction(CombatEnemy me, List<PartyMember> party, List<CombatEnemy> enemies)
    {
        _skills = me.Data.Skills;
        _party = party;
        _enemies = enemies;

        if (OthersHaveChosenAlready())
        {
            TryToCooperateWithOthers();
        }
        else
        {
            TryToMakeEffectiveAction();
        }
    }

    private void TryToCooperateWithOthers()
    {
        if (WasAbleToMakeACombo())
            return;
        if (WillDo(30))
            TryToGangUpWithOthers();
        else
            TryToMakeEffectiveAction();
    }

    private bool WasAbleToMakeACombo()
    {
        var offensiveSkills = GetOffensiveSkills();
        foreach (var enemy in _enemies)
        {
            foreach (var skill in offensiveSkills)
            {
                var attack = enemy.AttackHandler.Skill;
                if (attack != null && attack.GetType() == typeof(OffensiveSkill))
                {
                    OffensiveSkill offensiveSkill = (OffensiveSkill)enemy.AttackHandler.Skill;
                    if (skill.OffensiveData.Strength == offensiveSkill.OffensiveData.Element)
                    {
                        if (WillDo(90))
                        {
                            _attack = skill;
                            _target = enemy.AttackHandler.Targets[0];
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void TryToGangUpWithOthers()
    {
        foreach (var enemy in _enemies.Where(enemy => enemy.AttackHandler.Targets?.GetType() == typeof(PartyMember)))
        {
            _target = enemy.AttackHandler.Targets[0];
            ChooseRandomAttack();
        }
    }
    
    private bool OthersHaveChosenAlready()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy.AttackHandler.Targets != null && enemy.AttackHandler.Targets.GetType() == typeof(PartyMember)) 
                return true;
        }

        return false;
    }

    private void TryToMakeEffectiveAction()
    {
        if (HasEffectiveAttack())
        {
            ChooseEffectiveAction();
        }
        else
        {
            ChooseRandomAction();
        }
    }

    private bool HasEffectiveAttack()
    {
        var offensiveSkills = GetOffensiveSkills();
        foreach (var skill in offensiveSkills)
        {
            foreach (var partyMember in
                _party.Where(partyMember => partyMember.Element == skill.OffensiveData.Strength))
            {
                _weakTarget = partyMember;
                _effectiveSkill = skill;
                return true;
            }
        }

        return false;
    }

    private List<OffensiveSkill> GetOffensiveSkills() =>
        _skills.Where(skill => skill.GetType() == typeof(OffensiveSkill)).Cast<OffensiveSkill>().ToList();

    private void ChooseEffectiveAction()
    {
        if (WillDo(90))
        {
            _attack = _effectiveSkill;
            _target = _weakTarget;
        }
        else
        {
            ChooseRandomAction();
        }
    }

    private void ChooseRandomAction()
    {
        ChooseRandomAttack();

        var index = Random.Range(0, _party.Count);
        _target = _party[index];
    }

    private void ChooseRandomAttack()
    {
        var index = Random.Range(0, _skills.Count);
        _attack = _skills[index];
    }

    private bool WillDo(int percent)
    {
        var luckyNumber = Random.Range(0, 100);
        return luckyNumber < percent;
    }
}