using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Status/EntityData")]
public class EntityStatus : ScriptableObject
{
    public string entityName;
    public Stats stats;
    public GameObject model;
}

