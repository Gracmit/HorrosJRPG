public interface ICombatEntity
{
    bool Attacked { get; }
    bool Alive { get; }

    void TakeDamage();
    void Died();
    bool AttackChosen { get; }

    void ChooseAttack();
    void ResetChosenAttack();
    void Attack();
}