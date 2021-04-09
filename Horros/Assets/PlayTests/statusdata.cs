using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class statusdata
{
    [Test]
    public void can_save_enemies()
    {
        var statusManager = new GameObject().AddComponent<StatusManager>();
        statusManager.SetStatusData(ScriptableObject.CreateInstance<StatusData>());
        
        var enemyPool = new GameObject().AddComponent<EnemyPool>();
        var enemy = ScriptableObject.CreateInstance<CombatEnemyData>();
        var enemy2 = ScriptableObject.CreateInstance<CombatEnemyData>();
        enemyPool.AddEnemy(enemy);
        enemyPool.AddEnemy(enemy2);
        
        var player = new GameObject();
        statusManager.SetBattleData(player, enemyPool);
        
        Assert.AreEqual(enemyPool.EnemyCount, statusManager.StatusData.enemyGroup.Count);
    }

    [Test]
    public void saves_player_position()
    {
        var statusManager = new GameObject().AddComponent<StatusManager>();
        statusManager.SetStatusData(ScriptableObject.CreateInstance<StatusData>());
        
        var enemyPool = new GameObject().AddComponent<EnemyPool>();
        var player = new GameObject();
        player.transform.position = new Vector3(1, 2, 3);
        statusManager.SetBattleData(player, enemyPool);
        Assert.AreEqual(player.transform.position.x, statusManager.StatusData.position[0]);
        Assert.AreEqual(player.transform.position.y, statusManager.StatusData.position[1]);
        Assert.AreEqual(player.transform.position.z, statusManager.StatusData.position[2]);
    }
}
