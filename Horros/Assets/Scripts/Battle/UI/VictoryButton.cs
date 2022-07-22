using System;
using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    private void OnEnable()
    {
        BattleUIManager.Instance.EventHandler.ActivateWinButton();
    }

    public void ReturnToOverWorld()
    {
        StartCoroutine(LevelLoader.Instance.LoadLevelWithName(StatusManager.Instance.StatusData.SceneName));
        BattleUIManager.Instance.ToggleVictoryScreen(false);
        BattleUIManager.Instance.ToggleRunAwayScreen(false);
    }
}
