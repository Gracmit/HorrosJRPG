﻿using System.Collections.Generic;
using UnityEngine;

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

    public void SetBattleData(Collider other, EnemyPool enemyPool)
    {
        var partyPool = other.gameObject.GetComponent<PartyPool>();
        SetPartyData(partyPool);
        SetEnemyGroupData(enemyPool);

        Vector3 playerPosition = other.transform.position;
        _statusData.position[0] = playerPosition.x;
        _statusData.position[1] = playerPosition.y;
        _statusData.position[2] = playerPosition.z;
    }

    private void SetEnemyGroupData(EnemyPool enemyPool)
    {
        List<CombatEnemy> enemies = enemyPool.Enemies;
        List<EntityStatus> enemyStatus = new List<EntityStatus>();
        foreach (var enemy in enemies)
        {
            enemyStatus.Add(enemy.EnemyStatus);
        }
        
        _statusData.enemyGroupStatus = enemyStatus;
    }
    private void SetPartyData(PartyPool partyPool)
    {
        List<PartyMember> members = partyPool.Members;
        List<EntityStatus> partyStatus = new List<EntityStatus>();
        for (int i = 0; i < 3; i++)
        {
            if (members.Count > i)
            {
                partyStatus.Add(members[i].MemberStatus);
            }
        }
        
        _statusData.partyStatus = partyStatus;
    }
}