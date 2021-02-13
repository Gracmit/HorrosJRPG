using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private StatusManager _statusManager;
    private PartyPool _party;
    
    public static GameManager Instance => _instance;
    public List<PartyMember> ActiveParty => _party.GetActiveMembers();


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

        _party = GetComponent<PartyPool>();
        DontDestroyOnLoad(gameObject);
        
    }
}
