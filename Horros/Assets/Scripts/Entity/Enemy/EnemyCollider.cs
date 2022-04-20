using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyPool = GetComponent<EnemyPool>();
            var stateMachine = GetComponent<EnemyStateMachine>();
            if(stateMachine.CurrentState.GetType() == typeof(Roam))
                StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, stateMachine.Roamer.ID, EngageType.Ambush);
            else
                StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, stateMachine.Roamer.ID, EngageType.Danger);
                
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        }
    }
}
