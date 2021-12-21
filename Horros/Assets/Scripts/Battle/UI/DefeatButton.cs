using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatButton : MonoBehaviour
{
    public void RestartBattle()
    {
        StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        BattleUIManager.Instance.ToggleDefeatScreen(false);
    }
}
