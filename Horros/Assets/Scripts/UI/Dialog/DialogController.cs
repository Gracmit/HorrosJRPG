using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button[] _choiceButtons;
    [SerializeField] private float _textSpeed = 2;

    private Story _story;
    private CanvasGroup _canvasGroup;
    private bool _showing;
    private bool _writing;
    private string _currentLine;
    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        ToggleCanvasOff();
    }

    private void Update()
    {
        if (_showing && PlayerInput.Instance.Controls.UI.Submit.WasPressedThisFrame() && !_writing)
            StartCoroutine(RefreshView());
        else if (_showing && _writing && PlayerInput.Instance.Controls.UI.Submit.WasPressedThisFrame())
        {
            //StopCoroutine(ShowText());
            _writing = false;
            _storyText.SetText(_currentLine);
        }
    }


    [ContextMenu("Start Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        _story = new Story(dialog.text);
        StartCoroutine(RefreshView());
        ToggleCanvasOn();
    }

    private void ToggleCanvasOn()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _showing = true;
        PlayerInput.UseCursor();
        ControlsManager.Instance.FreezeMovement(true);
        ControlsManager.Instance.LockCamera(true);
    }

    private void ToggleCanvasOff()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _showing = false;
        PlayerInput.LockAndHideCursor();
        ControlsManager.Instance.FreezeMovement(false);
        ControlsManager.Instance.LockCamera(false);
    }

    private IEnumerator RefreshView()
    {
        if (_story.canContinue)
        {
            if(_story.currentChoices.Count == 0)
                ShowChoiceButtons();
            
            yield return StartCoroutine(ShowText());
            ShowChoiceButtons();
            yield break;
        }

        if (_story.currentChoices.Count == 0)
            ToggleCanvasOff();
        else
            ShowChoiceButtons();
        
    }

    private IEnumerator ShowText()
    {
        _currentLine = _story.Continue();
        _writing = true;
        var originalText = _currentLine;
        var alphaIndex = 0;
        HandleTags();


        foreach (var character in _currentLine.ToCharArray())
        {
            alphaIndex++;
            _storyText.SetText(originalText);
            var displayedText = _storyText.text.Insert(alphaIndex, "<color=#00000000>");
            _storyText.SetText(displayedText);
            yield return new WaitForSecondsRealtime(0.1f / _textSpeed);
            if(!_writing)
                yield break;
        }

        _writing = false;
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
                    StartCoroutine(RefreshView());
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

            if (tag.StartsWith("N."))
            {
                _nameText.SetText(tag.Remove(0, 2));
            }
        }
    }
}