using UnityEngine;

[CreateAssetMenu(fileName = "CombatEnemy", menuName = "CombatEntity/Enemy")]
public class CombatEnemy : ScriptableObject, ICombatEntity
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _stats = new Stats();
    [SerializeField] private EntityStatus _enemyStatus;
    public EntityStatus EnemyStatus => _enemyStatus;
}      

public interface ICombatEntity
{
    
}