using UnityEngine;

public class ActionListUI : MonoBehaviour
{
    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateActionsButton();
    }
}
