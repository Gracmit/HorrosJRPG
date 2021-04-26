using System;
using UnityEngine;

public class Lose : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        BattleUIManager.Instance.ToggleStatusPanels(false);
        BattleUIManager.Instance.ToggleDefeatScreen(true);
        Debug.Log("Lost");
    }

    public void OnExit()
    {
        
    }
}