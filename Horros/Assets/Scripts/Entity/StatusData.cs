using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Status/Data")]
public class StatusData : ScriptableObject
{
    public List<CombatEnemyData> EnemyGroup;
    public float[] Position = new float[3];
    public string SceneName;
    public int SpawnerID;
}
