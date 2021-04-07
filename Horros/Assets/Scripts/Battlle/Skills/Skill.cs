using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public virtual void HandleAttack(ICombatEntity attacker, ICombatEntity target)
    {
    }
}