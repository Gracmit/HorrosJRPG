using UnityEngine;

public class PlayerChoose : IState
{
    private GameObject _actionUI;

    public PlayerChoose(GameObject actionUI)
    {
        _actionUI = actionUI;
    }
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.ActiveEntity.ResetChosenAttack();
        BattleUIManager.Instance.ToggleActionList(true);
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        _actionUI.gameObject.SetActive(false);
    }
}