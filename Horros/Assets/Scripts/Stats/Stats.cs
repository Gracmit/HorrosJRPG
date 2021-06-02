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
            stat.Value += value;
        else
        {
            var newStat = new Stat {StatType = statType, Value = value};
            _stats.Add(newStat);
        }
    }

    public void Subtract(StatType statType, int value)
    {
        var stat = _stats.Find(x => x.StatType == statType);
        if (stat != null)
        {
            stat.Value -= value;
            if (stat.Value < 0)
            {
                stat.Value = 0;
            }
        }
    }

    public int GetValue(StatType statType)
    {
        return _stats.Find(x => x.StatType == statType).Value;
    }

    public void Replenish(StatType statType, int amount)
    {
        if (statType == StatType.HP)
        {
            var hp = _stats.Find(x => x.StatType == StatType.HP);
            var maxHp = _stats.Find(x => x.StatType == StatType.MaxHP);
            hp.Value += amount;
            if (hp.Value > maxHp.Value)
                hp.Value = maxHp.Value;
        }

        if (statType == StatType.MP)
        {
            var mp = _stats.Find(x => x.StatType == statType);
            var maxMp = _stats.Find(x => x.StatType == StatType.MaxMP);
            mp.Value += amount;
            if (mp.Value > maxMp.Value)
                mp.Value = maxMp.Value;
        }
    }

    public void FullHeal()
    {
        var hp = _stats.Find(x => x.StatType == StatType.HP);
        var maxHp = _stats.Find(x => x.StatType == StatType.MaxHP);
        hp.Value = maxHp.Value;
    }
}