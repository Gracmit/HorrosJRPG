using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnStatus _spawnStatus;
    private EnemySpawner[] _spawners;

    private void Start()
    {
        _spawners = FindObjectsOfType<EnemySpawner>();

        var id = StatusManager.Instance.StatusData.SpawnerID;
        if (id != 0)
            _spawnStatus.AddID(id);
        
        foreach (var spawner in _spawners)
        {
            if (!_spawnStatus.IdExists(spawner.ID))
                spawner.Spawn();
        }
    }
}