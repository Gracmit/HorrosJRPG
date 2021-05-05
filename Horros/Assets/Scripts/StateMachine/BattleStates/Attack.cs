using System;
using UnityEngine;

public class Attack : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleManager.Instance.ResetPartyIndex();
        Debug.Log("Changed State to PlayerAttack");
        BattleManager.Instance.TurnManager.SortEntities();
        BattleManager.Instance.TurnManager.Attack();
    }

    public void OnExit()
    {
        BattleManager.Instance.TurnManager.ResetAttack();
        BattleManager.Instance.TurnManager.NextTurn();
    }
}