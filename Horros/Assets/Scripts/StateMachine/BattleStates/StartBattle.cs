using System;
using UnityEngine;

public class StartBattle : IState
{
    public void Tick()
    {
        Debug.Log("Starting");
    }

    public void OnEnter()
    {
        BattleManager.Instance.InitializeBattleField();
    }

    public void OnExit()
    {
        
    }
}
