using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var enemyPool = this.GetComponent<EnemyPool>();
            StatusManager.Instance.SetBattleData(other, enemyPool);
            LevelLoader.Instance.LoadLevelWithName("Combat");
        }
    }
}
