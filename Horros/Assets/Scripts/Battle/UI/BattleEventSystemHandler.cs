using UnityEngine;
using UnityEngine.EventSystems;

public class BattleEventSystemHandler : MonoBehaviour
{
    [SerializeField] private GameObject _actionsFirstButton;
    [SerializeField] private GameObject _winButton;
    [SerializeField] private GameObject _loseButton;

    public void ActivateActionsButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_actionsFirstButton);
    }

    public void ActivateSkillButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void ActivateItemButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void ActivateWinButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_winButton);
    }
    
    public void ActivateLoseButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_loseButton);
    }
}
