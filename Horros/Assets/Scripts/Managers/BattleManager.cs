using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private TurnManager _turnManager;
    private bool _initializationCompleted;
    private bool _attackChosen;
    private ICombatEntity _activeEntity;
    private int _enemyCount;
    private int _partyCount;
    
    public static BattleManager Instance => _instance;
    public ICombatEntity ActiveEntity => _activeEntity;
    public int EnemyCount => _enemyCount;
    public int PartyCount => _partyCount;
    public TurnManager TurnManager => _turnManager;

    public bool Initialized() => _initializationCompleted;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _turnManager = new TurnManager();
        _activeEntity = ScriptableObject.CreateInstance<PartyMember>();
    }

    public void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        InstantiatePartyMembers();
        InstantiateEnemies(statusData);
        _activeEntity = _turnManager.GetNextEntity();
        _initializationCompleted = true;
    }


    private void InstantiatePartyMembers()
    {
        var spawnpointCounter = 0;
        foreach (var entity in GameManager.Instance.ActiveParty)
        {
            Instantiate(entity.Model, _playerSpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
            _turnManager.AddEntity(entity);
        }

        _partyCount = GameManager.Instance.ActiveParty.Count;
    }

    private void InstantiateEnemies(StatusData statusData)
    {
        var spawnpointCounter = 0;
        foreach (var entity in statusData.enemyGroup)
        {
            Instantiate(entity.Model, _enemySpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
            _turnManager.AddEntity(entity);
            BattleUIManager.Instance.AddEnemy(entity);
        }

        _enemyCount = statusData.enemyGroup.Count;
    }

    public void SetActiveEntity(ICombatEntity entity)
    {
        _activeEntity = entity;
    }

    public void SaveChosenAttack()
    {
        _activeEntity.ChooseAttack();
    }


    public void RemoveFromTurnQueue(PartyMember partyMember)
    {
        _partyCount--;
    }

    public void RemoveFromTurnQueue(CombatEnemy partyMember)
    {
        _enemyCount--;
    }
}