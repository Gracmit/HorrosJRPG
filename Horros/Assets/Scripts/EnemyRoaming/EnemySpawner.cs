using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private List<NavigationPoint> _navigationPoints;

    private void Start()
    {
        var enemy = Instantiate(_enemyToSpawn, transform);
        var roaming = enemy.GetComponent<EnemyRoaming>();
        StartCoroutine(roaming.SetNavigationPoints(_navigationPoints));
    }
}
