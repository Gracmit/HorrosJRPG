using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private bool _initializationCompleted;
    private bool _attackChosen;
    private Queue<ICombatEntity> _turnQueue = new Queue<ICombatEntity>();
    private Queue<ICombatEntity> _deadEntities = new Queue<ICombatEntity>();
    private int _enemyCount;
    private int _partyCount;
    private ICombatEntity _activeEntity;

    public static BattleManager Instance => _instance;

    public ICombatEntity ActiveEntity => _activeEntity;

    public int EnemyCount => _enemyCount;
    public int PartyCount => _partyCount;

    public bool Initialized() => _initializationCompleted;
    public ICombatEntity NextTurn
    {
        get
        {
            if (!_turnQueue.Peek().Alive)
            {
                var entity = _turnQueue.Dequeue();
                _deadEntities.Enqueue(entity);
            }
            return _turnQueue.Peek();
        }
    }

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

        _activeEntity = ScriptableObject.CreateInstance<PartyMember>();
    }

    public void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        InstantiatePartyMembers();
        InstantiateEnemies(statusData);
        _activeEntity = _turnQueue.Dequeue();
        _initializationCompleted = true;
    }


    private void InstantiatePartyMembers()
    {
        var spawnpointCounter = 0;
        foreach (var entity in GameManager.Instance.ActiveParty)
        {
            Instantiate(entity.Model, _playerSpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
            _turnQueue.Enqueue(entity);
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
            _turnQueue.Enqueue(entity);
        }

        _enemyCount = statusData.enemyGroup.Count;
    }

    public void SaveChosenAttack()
    {
        _activeEntity.ChooseAttack();
    }

    public void ChangeToNextTurn()
    {
        _turnQueue.Enqueue(_activeEntity);
        _activeEntity = _turnQueue.Dequeue();
        foreach (var entity in _deadEntities)
        {
            if (entity.GetType() == typeof(PartyMember))
            {
                _turnQueue.Enqueue(entity);
            }
        }
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