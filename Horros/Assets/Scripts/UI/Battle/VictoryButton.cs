using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    public void ReturnToOverWorld()
    {
        StartCoroutine(LevelLoader.Instance.LoadLevelWithName(StatusManager.Instance.StatusData.sceneName));
        BattleUIManager.Instance.ToggleVictoryScreen(false);
    }
}
