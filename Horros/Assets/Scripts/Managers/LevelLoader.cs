using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance;
    
    public static LevelLoader Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void LoadLevelWithName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
