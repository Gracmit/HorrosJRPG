using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private StatusManager _statusManager;
    private PartyPool _party;
    private Inventory _inventory;
    
    public static GameManager Instance => _instance;
    public List<PartyMember> ActiveParty => _party.GetActiveMembers();
    public Inventory Inventory => _inventory;


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

        _party = GetComponent<PartyPool>();
        _inventory = GetComponent<Inventory>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (InputHandler.Instance.Controls.Player.Test.WasPressedThisFrame())
        {
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Cell1-1"));
        }
    }
}
