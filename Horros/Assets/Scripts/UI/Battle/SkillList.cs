using System.Collections.Generic;
using UnityEngine;
/**
 * TODO: Pooling for buttons
 */
public class SkillList : MonoBehaviour
{
    [SerializeField] private GameObject _skillButton;
    private readonly List<GameObject> _buttons = new List<GameObject>();
    
    public void InstantiateButtons(List<Skill> skills)
    {
        if (_buttons.Count > 0)
        {
            DeleteOldButtons();
        }

        foreach (var skill in skills)
        {
            var button = Instantiate(_skillButton);
            _buttons.Add(button);

            var skillButton = button.GetComponent<SkillButton>();
            skillButton.SetSkill(skill);
            button.transform.SetParent(transform);
        }
    }

    private void DeleteOldButtons()
    {
        for (int i = _buttons.Count - 1; i >= 0; i--)
        {
            var button = _buttons[i];
            _buttons.Remove(button);
            Destroy(button);
        }
    }

    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateSkillButton(_buttons[0]);
    }
}