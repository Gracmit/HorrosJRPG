using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnStatus _spawnStatus;
    private EnemySpawner[] _spawners;

    
    private void Start()
    {
        Load(SceneManager.GetActiveScene(), LoadSceneMode.Additive);
        SceneManager.sceneLoaded += Load;
    }

    private void Load(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name is "UI")
            return;
        
        _spawners = FindObjectsOfType<EnemySpawner>();

        var id = StatusManager.Instance.StatusData.SpawnerID;
        if (id != 0)
            _spawnStatus.AddID(id);

        foreach (var spawner in _spawners)
        {
            if (!_spawnStatus.IdExists(spawner.ID))
                spawner.Spawn();
        }

        if (StatusManager.Instance.StatusData.PlayerPosition != Vector3.zero)
        {
            var player = FindObjectOfType<PlayerMovementController>();
            player.transform.position = StatusManager.Instance.StatusData.PlayerPosition;
            player.transform.rotation = StatusManager.Instance.StatusData.PlayerRotation;
        }
        
        var events =  StatusManager.Instance.StatusData.Events;
        foreach (var gameEvent in events)
        {
            GameEvent.RaiseEvent(gameEvent.name);
        }
    }
}