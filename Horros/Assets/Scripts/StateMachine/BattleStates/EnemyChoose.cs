using System;
using UnityEngine;

public class EnemyChoose : IState
{
    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnEnter()
    {
        BattleManager.Instance.ChangeToNextTurn();
        Debug.Log("Changed to EnemyChoose state");
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}