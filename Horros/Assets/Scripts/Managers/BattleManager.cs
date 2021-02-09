using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private bool _initializationCompleted;

    public static BattleManager Instance => _instance;
    public bool Initialized() => _initializationCompleted;

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

    public void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        InstantiatePartyMembers(statusData);
        InstantiateEnemies(statusData);
        _initializationCompleted = true;
    }


    private void InstantiatePartyMembers(StatusData statusData)
    {
        var spawnpointCounter = 0;
        foreach (var entity in statusData.partyStatus)
        {
            Instantiate(entity.model, _playerSpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
        }
    }
  
    private void InstantiateEnemies(StatusData statusData)
    {
        var spawnpointCounter = 0;
        foreach (var entity in statusData.enemyGroupStatus)
        {
            Instantiate(entity.model, _enemySpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
        }
    }

}
