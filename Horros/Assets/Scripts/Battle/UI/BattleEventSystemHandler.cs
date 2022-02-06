using UnityEngine;
using UnityEngine.EventSystems;

public class BattleEventSystemHandler : MonoBehaviour
{
    [SerializeField] private GameObject _actionsFirstButton;

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
}
