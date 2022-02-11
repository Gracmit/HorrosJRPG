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

        if (StatusManager.Instance.StatusData.PlayerPosition != Vector3.zero)
        {
            var player = FindObjectOfType<PlayerMovementController>();
            player.transform.position = StatusManager.Instance.StatusData.PlayerPosition;
            player.transform.rotation = StatusManager.Instance.StatusData.PlayerRotation;
        }
    }
}