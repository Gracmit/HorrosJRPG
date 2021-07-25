using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "CombatAI/SimpleOffensiveAi", fileName = "SimpleOffensiveAI", order = 0)]
public class SimpleOffensiveAI : CombatAI
{
    private ICombatEntity _target;
    private Skill _attack;
    
    private PartyMember _weakTarget;
    private Skill _effectiveSkill;
    private List<Skill> _skills;
    private List<PartyMember> _party;
    
    public override Skill GetSkill() => _attack;

    public override ICombatEntity GetTarget() => _target;

    public override void ChooseAction(CombatEnemy me, List<PartyMember> party, List<CombatEnemy> enemies)
    {
        _skills = me.Data.Skills;
        _party = party;
        if (HasEffectiveAttack(party))
        {
            ChooseEffectiveAction();
        }
        else
        {
            ChooseRandomAction();
        }
    }
    
    private bool HasEffectiveAttack(List<PartyMember> partyMembers)
    {
        var offensiveSkills = GetOffensiveSkills();
        foreach (var skill in offensiveSkills)
        {
            foreach (var partyMember in partyMembers.Where(partyMember => partyMember.Element == skill.OffensiveData.Strength))
            {
                _weakTarget = partyMember;
                _effectiveSkill = skill;
                return true;
            }
        }
        return false;
    }

    private List<OffensiveSkill> GetOffensiveSkills() => _skills.Where(skill => skill.GetType() == typeof(OffensiveSkill)).Cast<OffensiveSkill>().ToList();

    private void ChooseEffectiveAction()
    {
        var luckyNumber = Random.Range(0, 100);
        if (luckyNumber < 90)
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
        var index = Random.Range(0, _skills.Count);
        _attack = _skills[index];

        index = Random.Range(0, _party.Count);
        _target = _party[index];
    }
}
