using UnityEngine;

public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _stats;
    [SerializeField] private GameObject _model;
    
    public GameObject Model => _model;
    public string Name => _name;
    public Stats Stats { get; set; }
}
