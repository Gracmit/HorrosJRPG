using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] Quest _selectedQuest;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _currentObjectivesText;
    Step selectedStep => _selectedQuest.CurrenStep;

    [ContextMenu("Bind")]
    public void Bind()
    {
        _nameText.SetText(_selectedQuest.Name);
        _descriptionText.SetText(_selectedQuest.Description);
        
        DisplayStepsInstructionsAndObjectives();
    }

    void DisplayStepsInstructionsAndObjectives()
    {
        StringBuilder builder = new StringBuilder();

        if (selectedStep != null)
        {
            builder.AppendLine(selectedStep.Instructions);
            foreach (var objective in selectedStep.Objectives)
            {
                builder.AppendLine(objective.ToString());
            }
        }

        _currentObjectivesText.SetText(builder.ToString());
    }

    public void SelectQuest(Quest quest)
    {
        if(_selectedQuest)
            _selectedQuest.Changed -= DisplayStepsInstructionsAndObjectives;
        
        _selectedQuest = quest;
        Bind();

        _selectedQuest.Changed += DisplayStepsInstructionsAndObjectives;

    }
}