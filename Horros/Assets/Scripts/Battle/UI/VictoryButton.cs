using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    public void ReturnToOverWorld()
    {
        StartCoroutine(LevelLoader.Instance.LoadLevelWithName(StatusManager.Instance.StatusData.SceneName));
        BattleUIManager.Instance.ToggleVictoryScreen(false);
        BattleUIManager.Instance.ToggleRunAwayScreen(false);
    }
}
