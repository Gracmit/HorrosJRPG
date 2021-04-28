using UnityEngine;

public class PlayerChoose : IState
{

    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.SetActiveEntity();
        BattleUIManager.Instance.ToggleActionList(true);
        BattleUIManager.Instance.InstantiateSkillButtons(BattleManager.Instance.ActiveMember.Data.Skills);
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        //BattleUIManager.Instance.ToggleActionList(false);
        BattleUIManager.Instance.StackHandler.ClearStack();
    }
}