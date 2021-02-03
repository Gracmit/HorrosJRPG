using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<CombatEnemy> _enemies = new List<CombatEnemy>();
    public double EnemyCount => _enemies.Count;
    public List<CombatEnemy> Enemies => _enemies;
}