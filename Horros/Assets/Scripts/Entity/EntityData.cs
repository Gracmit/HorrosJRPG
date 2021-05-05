using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Stats _stats;
    [SerializeField] private GameObject _model;
    [SerializeReference] private List<Skill> _skills;
    
    public GameObject Model => _model;
    public string Name => _name;
    public Stats Stats => _stats;
    public List<Skill> Skills => _skills;
}
