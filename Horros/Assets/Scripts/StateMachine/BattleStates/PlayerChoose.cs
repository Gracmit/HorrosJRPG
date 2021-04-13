using UnityEngine;

public class PlayerChoose : IState
{

    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleUIManager.Instance.ToggleActionList(true);
        BattleUIManager.Instance.InstantiateSkillButtons(BattleManager.Instance.ActiveEntity.Data.Skills);
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        BattleUIManager.Instance.ToggleActionList(false);
    }
}