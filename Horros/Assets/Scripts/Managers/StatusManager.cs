using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SetStatusData(StatusData data)
    {
        _statusData = data;
    }

    public void SetBattleData(GameObject player, EnemyPool enemyPool, int spawnerId)
    {
        _statusData.EnemyGroup = enemyPool.Enemies;

        Vector3 playerPosition = player.transform.position;
        _statusData.Position[0] = playerPosition.x;
        _statusData.Position[1] = playerPosition.y;
        _statusData.Position[2] = playerPosition.z;

        _statusData.SceneName = SceneManager.GetActiveScene().name;
        _statusData.SpawnerID = spawnerId;
    }
}