using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAI : ScriptableObject
{
    public abstract void ChooseAction(CombatEnemy me, List<PartyMember> party, List<CombatEnemy> enemies);
    public abstract Skill GetSkill();
    public abstract ICombatEntity GetTarget();
}