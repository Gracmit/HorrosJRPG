public interface ICombatEntity
{
    bool Alive { get; }
    EntityData Data { get; }
    void TakeDamage();
    void Died();
}