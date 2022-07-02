using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _description;
    [Tooltip("Designer/programmer notes, not visible to player")]
    [SerializeField] string _notes;
    public List<Step> Steps;
    public string Name => _name;
    public string Description => _description;
}

[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public List<Objectives> Objectives;
    public string Instructions => _instructions;
}

[Serializable]
public class Objectives
{
    [SerializeField] ObjectiveType _objectiveType;
    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }

    public override string ToString() => _objectiveType.ToString();
}
