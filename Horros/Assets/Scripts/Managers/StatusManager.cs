using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private static StatusManager _instance;

    [SerializeField] private StatusData _statusData;
    
    public static StatusManager Instance => _instance;

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

    public void setBattleData(Collider other, PartyPool partypool)
    {
        List<PartyMember> members = partypool.Members;
        _statusData.member = members[0];
        Vector3 enemyPosition = other.transform.position;
        _statusData.position[0] = enemyPosition.x;
        _statusData.position[1] = enemyPosition.y;
        _statusData.position[2] = enemyPosition.z;
    }
}
