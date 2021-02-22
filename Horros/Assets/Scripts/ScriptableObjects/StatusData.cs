using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Status/Data")]
public class StatusData : ScriptableObject
{
    public List<CombatEnemy> enemyGroup;
    public float[] position = new float[3];
}
