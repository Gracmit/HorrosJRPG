using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyPool = GetComponent<EnemyPool>();
            var stateMachine = GetComponent<EnemyStateMachine>();
            StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, stateMachine.Roamer.ID);
            
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        }
    }
}
