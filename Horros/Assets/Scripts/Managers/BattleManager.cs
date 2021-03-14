﻿using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    private static BattleManager _instance;
    private TurnManager _turnManager;
    private AttackHandler _attackHandler = new AttackHandler();
    private bool _initializationCompleted;
    private ICombatEntity _activeEntity;
    private int _enemyCount;
    private List<PartyMember> _activeParty = new List<PartyMember>();

    public static BattleManager Instance => _instance;
    public ICombatEntity ActiveEntity => _activeEntity;
    public int EnemyCount => _enemyCount;
    public int PartyCount => _activeParty.Count;
    public TurnManager TurnManager => _turnManager;

    public AttackHandler AttackHandler => _attackHandler;
    public List<PartyMember> Party => _activeParty;

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
        _activeEntity = new PartyMember(ScriptableObject.CreateInstance<PartyMemberData>());
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
            _activeParty.Add(entity);
        }
    }

    private void InstantiateEnemies(StatusData statusData)
    {
        var spawnpointCounter = 0;
        foreach (var entityData in statusData.enemyGroup)
        {
            var enemy = new CombatEnemy(entityData);
            Instantiate(entityData.Model, _enemySpawnpoints[spawnpointCounter]);
            spawnpointCounter++;
            _turnManager.AddEntity(enemy);
            BattleUIManager.Instance.AddEnemy(enemy);
        }

        _enemyCount = statusData.enemyGroup.Count;
    }

    public void SetActiveEntity(ICombatEntity entity)
    {
        _activeEntity = entity;
        _attackHandler.SaveAttacker(_activeEntity);
        Debug.Log(_activeEntity.Data.Name);
    }

    public void SaveChosenAttack()
    {
        _attackHandler.SaveAttack();
    }


    public void RemoveFromTurnQueue(PartyMember partyMember)
    {
        _activeParty.Remove(partyMember);
    }

    public void RemoveFromTurnQueue(CombatEnemy partyMember)
    {
        _enemyCount--;
    }
}