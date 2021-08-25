internal class RunAway : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        BattleUIManager.Instance.ToggleActionList(false);
        BattleUIManager.Instance.ToggleStatusPanels(false);
        BattleUIManager.Instance.ToggleRunAwayScreen(true);
    }

    public void OnExit()
    {
    }
}