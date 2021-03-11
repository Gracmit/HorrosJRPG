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
    }

    public void OnExit()
    {
        BattleManager.Instance.TurnManager.ChangeToNextTurn();
    }
}
