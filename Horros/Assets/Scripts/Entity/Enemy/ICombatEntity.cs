using UnityEngine;

public interface ICombatEntity
{
    bool Alive { get; }
    EntityData Data { get; }

    GameObject CombatAvatar { get; set; }

    ElementType Element { get;}
    StatusEffect Effect { get; }
    bool Attacked{ get;}

    AttackHandler AttackHandler { get; }

    void TakeDamage(int damage);
    void Die();
    void ChangeElement(StatusEffect effect);        
    void AddBuff(BuffSkillData buff);
    void Attack();
    void SetAttackHandler();
    void ResetAttack();
    void Highlight();
    void UnHighlight();
    int GetStatValue(StatType type);
    void CheckBuffs();
}