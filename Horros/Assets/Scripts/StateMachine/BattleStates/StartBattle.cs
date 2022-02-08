using UnityEngine;

public class StartBattle : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Starting");
        BattleManager.Instance.InitializeBattleField();
        BattleCameraManager.Instance.StartIntro();
    }

    public void OnExit()
    {
        BattleUIManager.Instance.ToggleStatusPanels(true);
    }
}
