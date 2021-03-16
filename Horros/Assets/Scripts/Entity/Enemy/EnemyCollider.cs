﻿using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyPool = GetComponent<EnemyPool>();
            StatusManager.Instance.SetBattleData(other, enemyPool);
            
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        }
    }
}
