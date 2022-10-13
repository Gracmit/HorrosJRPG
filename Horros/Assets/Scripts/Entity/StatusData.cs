using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Status/Data")]
public class StatusData : ScriptableObject
{
    public List<CombatEnemyData> EnemyGroup;
    public string SceneName;
    public int SpawnerID;
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public EngageType EngageType;
    public List<GameEvent> Events;

// ToDO: Delete this
    private void OnEnable()
    {
        Events = new List<GameEvent>(); 
    }
}

public enum EngageType
{
    Ambush, 
    Normal,
    Danger
}
