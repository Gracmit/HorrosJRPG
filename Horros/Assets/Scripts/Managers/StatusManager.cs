using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusManager : MonoBehaviour
{
    private static StatusManager _instance;

    [SerializeField] private StatusData _statusData;

    public static StatusManager Instance => _instance;
    public StatusData StatusData => _statusData;

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

    public void SetStatusData(StatusData data)
    {
        _statusData = data;
    }

    public void SetBattleData(GameObject player, EnemyPool enemyPool, int spawnerId, EngageType engageType,
        List<GameEvent> gameEvents)
    {
        _statusData.EnemyGroup = enemyPool.Enemies;
        _statusData.PlayerPosition = player.transform.position;
        _statusData.PlayerRotation = player.transform.rotation;
        _statusData.SceneName = SceneManager.GetActiveScene().name;
        _statusData.SpawnerID = spawnerId;
        _statusData.EngageType = engageType;
        _statusData.Events = gameEvents;
    }
}