using System;
using UnityEngine;

[Serializable]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;

    public string Name => _name;
}