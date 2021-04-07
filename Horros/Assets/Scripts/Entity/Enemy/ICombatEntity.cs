using UnityEngine;

public interface ICombatEntity
{
    bool Alive { get; }
    EntityData Data { get; }

    GameObject CombatAvatar { get; set; }
    ElementType Element { get;}
    void TakeDamage(int damage);
    void Die();
    void ChangeElement(ElementType element);
    void AddBuff(BuffSkillData buff);
}