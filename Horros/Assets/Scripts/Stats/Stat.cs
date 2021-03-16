using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private StatType _statType;
    [SerializeField] private int _value;

    public StatType StatType
    {
        get => _statType;
        set => _statType = value;
    }

    public int Value
    {
        get => _value;
        set => _value = value;
    }
}