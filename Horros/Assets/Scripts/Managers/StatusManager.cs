using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private static StatusManager _instance;

    [SerializeField] private StatusData _statusData;

    public static StatusManager Instance => _instance;
    public StatusData StatusData => _statusData;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetBattleData(Collider other, EnemyPool enemyPool)
    {
        _statusData.enemyGroup = enemyPool.Enemies;

        Vector3 playerPosition = other.transform.position;
        _statusData.position[0] = playerPosition.x;
        _statusData.position[1] = playerPosition.y;
        _statusData.position[2] = playerPosition.z;
    }
}