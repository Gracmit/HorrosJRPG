﻿using System;
using UnityEngine;

public class EnemyChoose : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Changed to EnemyChoose state");
        BattleManager.Instance.ActiveEntity.ChooseAttack();
    }

    public void OnExit()
    {
        
    }
}