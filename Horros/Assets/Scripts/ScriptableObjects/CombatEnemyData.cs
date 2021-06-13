using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEnemy", menuName = "CombatEntity/Enemy")]
public class CombatEnemyData : EntityData
{
    [SerializeField] private List<Item> _lootTable;

    public List<Item> Loot => _lootTable;
}
