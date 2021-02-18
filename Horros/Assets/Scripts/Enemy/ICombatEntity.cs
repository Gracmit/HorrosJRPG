public interface ICombatEntity
{
    bool Attacked { get;}
    bool Alive { get;}

    void TakeDamage();
    void Died();
}