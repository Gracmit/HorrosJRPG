using System;
using UnityEngine;

public class PlayerChoose : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.ChangeToNextTurn();
        Debug.Log("Changed to PlayerChoose state");
    }

    public void OnExit()
    {
        BattleManager.Instance.AttackNotChosen();
    }
}