using System;
using UnityEngine;

public class EnemyAttack : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        BattleManager.Instance.ActiveEntity.Attack();
        Debug.Log("Changed State to EnemyAttack");
    }

    public void OnExit()
    {
        BattleManager.Instance.ActiveEntity.ResetChosenAttack();
        BattleManager.Instance.ChangeToNextTurn();
    }
}