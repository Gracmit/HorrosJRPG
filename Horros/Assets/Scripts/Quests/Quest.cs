using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public event Action Changed;

    [SerializeField] string _name;
    [SerializeField] string _description;

    [Tooltip("Designer/programmer notes, not visible to player")] [SerializeField]
    string _notes;

    int _currentStepIndex;

    public List<Step> Steps;
    public string Name => _name;
    public string Description => _description;
    public Step CurrenStep => Steps[_currentStepIndex];

    private void OnEnable()
    {
        _currentStepIndex = 0;
        foreach (var step in Steps)
        {
            foreach (var objective in step.Objectives)
            {
                if (objective.GameFlag != null)
                {
                    objective.GameFlag.Changed += HandleFlagChanged;
                }
            }
        }
    }

    private void HandleFlagChanged()
    {
        TryProgress();
        Changed?.Invoke();
    }

    private void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if (currentStep.HasObjectivesCompleted())
        {
            _currentStepIndex++;
            Changed?.Invoke();
        }
    }

    private Step GetCurrentStep() => Steps[_currentStepIndex];
}

[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public List<Objectives> Objectives;
    public string Instructions => _instructions;

    public bool HasObjectivesCompleted()
    {
        return Objectives.TrueForAll(t => t.IsCompleted);
    }
}

[Serializable]
public class Objectives
{
    [SerializeField] ObjectiveType _objectiveType;
    [SerializeField] GameFlag _gameFlag;

    public GameFlag GameFlag => _gameFlag;
    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }

    public bool IsCompleted
    {
        get
        {
            switch (_objectiveType)
            {
                case ObjectiveType.Flag: return _gameFlag.Value;
                default: return false;
            }
        }
    }


    public override string ToString()
    {
        switch (_objectiveType)
        {
            case ObjectiveType.Flag: return _gameFlag.name;
            default: return _objectiveType.ToString();
        }
    }
}