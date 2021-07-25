using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatEnemy", menuName = "CombatEntity/Enemy")]
public class CombatEnemyData : EntityData
{
    [SerializeField] private List<Item> _lootTable;
    [SerializeField] private CombatAI _ai;
    public List<Item> Loot => _lootTable;
    public CombatAI AI => _ai;
}
