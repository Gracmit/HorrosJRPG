using System;
using UnityEngine;

public class PlayerAttack : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Debug.Log("Changed State to PlayerAttack");
        BattleManager.Instance.AttackHandler.Attack();
    }

    public void OnExit()
    {
        BattleManager.Instance.TurnManager.ChangeToNextTurn();
    }
}