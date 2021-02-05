using UnityEngine;

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
