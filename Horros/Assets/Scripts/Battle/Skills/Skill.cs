using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public virtual IEnumerator HandleAttack(ICombatEntity attacker, List<ICombatEntity> targets)
    {
        yield return null;
    }

    public virtual SkillData Data { get; }
    
}