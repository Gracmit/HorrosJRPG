using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private List<GameEvent> _events;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var enemyPool = GetComponent<EnemyPool>();
            var stateMachine = GetComponent<EnemyStateMachine>();
            if(stateMachine.CurrentState.GetType() == typeof(Roam))
                StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, stateMachine.Roamer.ID, EngageType.Ambush, _events);
            else
                StatusManager.Instance.SetBattleData(other.gameObject, enemyPool, stateMachine.Roamer.ID, EngageType.Danger, _events);
                
            StartCoroutine(LevelLoader.Instance.LoadLevelWithName("Combat"));
        }
    }

    public void SetEvents(List<GameEvent> events)
    {
        _events = events;
    }
}
