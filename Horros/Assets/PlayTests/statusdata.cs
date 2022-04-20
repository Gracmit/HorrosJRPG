using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class statusdata
{
    [Test]
    public void can_save_enemies(EngageType engageType)
    {
        var statusManager = new GameObject().AddComponent<StatusManager>();
        statusManager.SetStatusData(ScriptableObject.CreateInstance<StatusData>());
        
        var enemyPool = new GameObject().AddComponent<EnemyPool>();
        var enemy = ScriptableObject.CreateInstance<CombatEnemyData>();
        var enemy2 = ScriptableObject.CreateInstance<CombatEnemyData>();
        enemyPool.AddEnemy(enemy);
        enemyPool.AddEnemy(enemy2);
        
        var player = new GameObject();
        statusManager.SetBattleData(player, enemyPool, 1, engageType);
        
        Assert.AreEqual(enemyPool.EnemyCount, statusManager.StatusData.EnemyGroup.Count);
    }

    [Test]
    public void saves_player_position(EngageType engageType)
    {
        var statusManager = new GameObject().AddComponent<StatusManager>();
        statusManager.SetStatusData(ScriptableObject.CreateInstance<StatusData>());
        
        var enemyPool = new GameObject().AddComponent<EnemyPool>();
        var player = new GameObject();
        player.transform.position = new Vector3(1, 2, 3);
        statusManager.SetBattleData(player, enemyPool, 1, engageType);
        Assert.AreEqual(player.transform.position, statusManager.StatusData.PlayerPosition);
    }

    [Test]
    public void can_resave_position_and_enemies(EngageType engageType, EngageType engageType1)
    {
        var statusManager = new GameObject().AddComponent<StatusManager>();
        statusManager.SetStatusData(ScriptableObject.CreateInstance<StatusData>());
        
        var enemyPool = new GameObject().AddComponent<EnemyPool>();
        var enemy = ScriptableObject.CreateInstance<CombatEnemyData>();
        var enemy2 = ScriptableObject.CreateInstance<CombatEnemyData>();
        enemyPool.AddEnemy(enemy);
        
        var player = new GameObject();
        player.transform.position = new Vector3(1, 2, 3);
        statusManager.SetBattleData(player, enemyPool, 1, engageType);
        
        enemyPool.AddEnemy(enemy2);
        player.transform.position = new Vector3(4,5,6);
        statusManager.SetBattleData(player, enemyPool, 1, engageType1);
        Assert.AreEqual(player.transform.position, statusManager.StatusData.PlayerPosition);
        Assert.AreEqual(enemyPool.EnemyCount, statusManager.StatusData.EnemyGroup.Count);
    }
}
