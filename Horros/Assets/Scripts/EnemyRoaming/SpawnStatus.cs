using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnStatus", fileName = "SpawnStatus")]
internal class SpawnStatus : ScriptableObject
{
    [SerializeField] private List<int> _defeatedIds = new List<int>();

    public bool IdExists(int spawnerID) => _defeatedIds.Exists(x => x == spawnerID);

    public void AddID(int id)
    {
        if (!IdExists(id))
            //_defeatedIds.Add(id);

        StatusManager.Instance.StatusData.SpawnerID = 0;
    }
}