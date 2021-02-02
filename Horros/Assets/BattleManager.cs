﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    void Start()
    {
        InitializeBattleField();
    }

    private void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        InstantiatePartyMembers(statusData);
        
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
}
