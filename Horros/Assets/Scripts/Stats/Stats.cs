using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Stats", menuName = "Stat/StatsContainer")]
public class Stats : ScriptableObject
{
    [SerializeField] private List<Stat> _stats = new List<Stat>();

    public void Add(StatType statType, int value)
    {
        var stat = _stats.Find(x => x.StatType == statType);
        if (stat != null)
            stat.Value = value;
        else
        {
            var newStat = new Stat {StatType = statType, Value = value};
            _stats.Add(newStat);
        }
    }
    
    public void Remove(StatType statType, int value)
    {
        var stat = _stats.Find(x => x.StatType == statType);
        if (stat != null)
            stat.Value -= value;
    }

    public int GetValue(StatType statType)
    {
        return _stats.Find(x => x.StatType == statType).Value;
    }
}