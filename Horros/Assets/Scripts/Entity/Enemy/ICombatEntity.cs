using UnityEngine;

public interface ICombatEntity
{
    bool Alive { get; }
    EntityData Data { get; }

    GameObject CombatAvatar { get; set; }
    void TakeDamage();
    void Die();
}