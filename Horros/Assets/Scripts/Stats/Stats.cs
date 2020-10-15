using System.Collections.Generic;

public class Stats
{
    private readonly Dictionary<StatType, int> _stats = new Dictionary<StatType, int>();

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
