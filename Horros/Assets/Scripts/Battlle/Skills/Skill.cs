using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public virtual IEnumerator HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
        yield return null;
    }

    public virtual SkillData Data { get; }
}