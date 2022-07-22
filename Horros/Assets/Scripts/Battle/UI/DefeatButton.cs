using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatButton : MonoBehaviour
{
    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateLoseButton();
    }

    public void RestartBattle()
    {
        BattleUIManager.Instance.ToggleDefeatScreen(false);
        StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
    }
    
}
