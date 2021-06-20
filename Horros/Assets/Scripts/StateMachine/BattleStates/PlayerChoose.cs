using UnityEngine;

public class PlayerChoose : IState
{
    private readonly Inventory _inventory;

    public PlayerChoose(Inventory inventory)
    {
        _inventory = inventory;
    }
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.SetActiveEntity();
        BattleUIManager.Instance.ToggleActionList(true);
        BattleUIManager.Instance.InstantiateSkillButtons(BattleManager.Instance.ActiveMember.Data.Skills);
        BattleUIManager.Instance.InstantiateItemButtons(_inventory.GetConsumables());
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        //BattleUIManager.Instance.ToggleActionList(false);
        BattleUIManager.Instance.StackHandler.ClearStack();
    }
}