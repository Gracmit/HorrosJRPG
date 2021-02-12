using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private bool _initializationCompleted;
    private bool _attackChosen;
    private Queue<BattleEntity> _entityTurn;
    private BattleEntity _activeEntity;

    public static BattleManager Instance => _instance;
    public bool Initialized() => _initializationCompleted;
    public bool AttackChosen() => _attackChosen;

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

    public void SaveChosenAttack()
    {
        _attackChosen = true;
    }

    public void AttackNotChosed()
    {
        _attackChosen = false;
    }
}

public class BattleEntity
{
    public string Name;
    public Stats stats;
    public bool partyMember;
}
