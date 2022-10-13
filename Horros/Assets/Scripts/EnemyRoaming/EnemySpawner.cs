using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private List<NavigationPoint> _navigationPoints;
    [SerializeField] private int _id;
    [SerializeField] private List<GameEvent> _events;
    public int ID => _id;

    public void Spawn()
    {
        var enemy = Instantiate(_enemyToSpawn, transform);
        var roaming = enemy.GetComponent<EnemyStateMachine>().Roamer;
        roaming.SetID(_id);
        StartCoroutine(roaming.SetNavigationPoints(_navigationPoints));
        var collider = enemy.GetComponent<EnemyCollider>();
        collider.SetEvents(_events);
    }
}
