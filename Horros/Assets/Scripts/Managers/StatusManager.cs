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

    public void SetBattleData(Collider other, PartyPool partypool)
    {
        SetPartyData(partypool);
        
        Vector3 playerPosition = other.transform.position;
        _statusData.position[0] = playerPosition.x;
        _statusData.position[1] = playerPosition.y;
        _statusData.position[2] = playerPosition.z;
    }

    private void SetPartyData(PartyPool partypool)
    {
        List<PartyMember> members = partypool.Members;
        List<EntityStatus> partyStatus = new List<EntityStatus>();
        for (int i = 0; i < 3; i++)
        {
            if (members[i].MemberStatus != null)
            {
                partyStatus.Add(members[i].MemberStatus);
            }
        }
        
        _statusData.partyStatus = partyStatus;
    }
}