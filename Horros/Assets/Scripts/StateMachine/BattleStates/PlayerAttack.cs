using System;
using UnityEngine;

public class PlayerAttack : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.ActiveEntity.Attack();
        Debug.Log("Changed State to PlayerAttack");
    }

    public void OnExit()
    {
        BattleManager.Instance.ActiveEntity.ResetChosenAttack();
        BattleManager.Instance.ChangeToNextTurn();
    }
}