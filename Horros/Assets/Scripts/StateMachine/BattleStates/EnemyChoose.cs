using System;
using UnityEngine;

public class EnemyChoose : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Changed to EnemyChoose state");
        var activeEntity = (CombatEnemy)BattleManager.Instance.ActiveEntity;
        activeEntity.ChooseAttack();
    }

    public void OnExit()
    {
        
    }
}