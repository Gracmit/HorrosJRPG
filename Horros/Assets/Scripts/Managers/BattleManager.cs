using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private bool _initializationCompleted;
    private bool _attackChosen;
    private Queue<ICombatEntity> _turnQueue;
    private ICombatEntity _activeEntity;

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
        InstantiatePartyMembers();
        InstantiateEnemies(statusData);
        _initializationCompleted = true;
    }


    private void InstantiatePartyMembers()
    {
        var spawnpointCounter = 0;
        foreach (var entity in GameManager.Instance.ActiveParty)
        {
            Instantiate(entity.Model, _playerSpawnpoints[spawnpointCounter]);
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

    public void AttackNotChosen()
    {
        _attackChosen = false;
    }
}