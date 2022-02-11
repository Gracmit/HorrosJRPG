using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyPool = GetComponent<EnemyPool>();
            var roaming = GetComponent<EnemyRoaming>();
            StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, roaming.ID);
            
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        }
    }
}
