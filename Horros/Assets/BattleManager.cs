using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        InitializeBattleField();
    }

    private void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        Instantiate(statusData.gameObject, _playerSpawnpoints[0]);
    }
}
