using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<CombatEnemyData> _enemies = new List<CombatEnemyData>();
    public double EnemyCount => _enemies.Count;
    public List<CombatEnemyData> Enemies => _enemies;
}