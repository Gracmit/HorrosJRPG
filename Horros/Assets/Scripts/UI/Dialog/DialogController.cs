using System;
using System.Text;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private Button[] _choiceButtons;

    private Story _story;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        ToggleCanvasOff();
    }


    [ContextMenu("Start Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        _story = new Story(dialog.text);
        RefreshView();
        ToggleCanvasOn();
    }

    private void ToggleCanvasOn()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
    
    private void ToggleCanvasOff()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();
        while (_story.canContinue)
        {
            storyTextBuilder.AppendLine(_story.Continue());
            HandleTags();
        }

        _storyText.SetText(storyTextBuilder);

        if (_story.currentChoices.Count == 0)
            ToggleCanvasOff();
        else
            ShowChoiceButtons();
    }

    private void ShowChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Length; i++)
        {
            var button = _choiceButtons[i];
            button.gameObject.SetActive(i < _story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if (i < _story.currentChoices.Count)
            {
                var choice = _story.currentChoices[i];
                button.GetComponentInChildren<TMP_Text>().SetText(choice.text);
                button.onClick.AddListener(() =>
                {
                    _story.ChooseChoiceIndex(choice.index);
                    RefreshView();
                });
            }
        }
    }

    private void HandleTags()
    {
        foreach (var tag in _story.currentTags)
        {
            if (tag.StartsWith("E."))
            {
                var eventName = tag.Remove(0, 2);
                GameEvent.RaiseEvent(eventName);
            }
        }
    }
}