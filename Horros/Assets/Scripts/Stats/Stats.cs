using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Dictionary<StatType, int> _stats = new Dictionary<StatType, int>();

    public void Add(StatType statType, int value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] += value;
        else
            _stats[statType] = value;
    }


    public void Remove(StatType statType, int value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] -= value;
    }

    public int Get(StatType statType)
    {
        return _stats[statType];
    }
}
