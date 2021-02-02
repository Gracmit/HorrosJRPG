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
            var partyPool = other.gameObject.GetComponent<PartyPool>();
            StatusManager.Instance.SetBattleData(other, partyPool);
            LevelLoader.Instance.LoadLevelWithName("Combat");
        }
    }
}
