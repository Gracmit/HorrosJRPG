using System;
using UnityEngine;

public class EnemyAttack : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Changed State to EnemyAttack");
        BattleManager.Instance.AttackHandler.Attack();
    }

    public void OnExit()
    {
        BattleManager.Instance.TurnManager.ChangeToNextTurn();
    }
}