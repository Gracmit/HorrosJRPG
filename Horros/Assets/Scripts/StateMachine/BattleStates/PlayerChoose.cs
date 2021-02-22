using UnityEngine;

public class PlayerChoose : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.ActiveEntity.ResetChosenAttack();
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        
    }
}