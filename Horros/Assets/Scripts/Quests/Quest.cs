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
}

[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public List<Objectives> Objectives;
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
}
