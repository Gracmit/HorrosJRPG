using System.Collections;
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


    public IEnumerator LoadLevelWithName(string levelName)
    {
        var operation = SceneManager.LoadSceneAsync(levelName);
        var operation2 = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);

        while (!operation.isDone && !operation2.isDone)
        {
            yield return null;
        }

        yield return null;
    }
}
