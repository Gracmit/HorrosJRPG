using System;
using UnityEngine;

public class Win : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        BattleUIManager.Instance.ToggleStatusPanels(false);
        BattleManager.Instance.GiveLoot();
        BattleUIManager.Instance.ToggleVictoryScreen(true);
        Debug.Log("Won");
    }

    public void OnExit()
    {
    }
}