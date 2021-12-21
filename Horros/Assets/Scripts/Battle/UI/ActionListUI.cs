using UnityEngine;
using UnityEngine.EventSystems;

public class ActionListUI : MonoBehaviour
{
    [SerializeField] private GameObject _firstButton;

    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateActionsButton();
    }
}
