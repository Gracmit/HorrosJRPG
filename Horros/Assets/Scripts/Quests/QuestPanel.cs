using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] Quest _selectedQuest;
    [SerializeField] Step _selectedStep;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _currentObjectivesText;

    [ContextMenu("Bind")]
    public void Bind()
    {
        _nameText.SetText(_selectedQuest.Name);
        _descriptionText.SetText(_selectedQuest.Description);
        
        _selectedStep = _selectedQuest.Steps.FirstOrDefault();
        if (_selectedStep == null) return;
        
        StringBuilder builder = new StringBuilder();
        builder.AppendLine(_selectedStep.Instructions);
        foreach (var objective in _selectedStep.Objectives)
        {
            builder.AppendLine(objective.ToString());
        }
        _currentObjectivesText.SetText(builder.ToString());
    }
}