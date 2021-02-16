using UnityEngine;

[CreateAssetMenu(fileName = "CombatEnemy", menuName = "CombatEntity/Enemy")]
public class CombatEnemy : ScriptableObject, ICombatEntity
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _stats = new Stats();
    [SerializeField] private EntityStatus _enemyStatus;
    private bool _attacked;
    public EntityStatus EnemyStatus => _enemyStatus;
    public bool Attacked => _attacked;
}