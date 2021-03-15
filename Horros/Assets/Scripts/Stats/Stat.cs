using System;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField] private StatType _statType;
    [SerializeField] private int _value;

    public StatType StatType { get; set; }
    public int Value { get; set; }
}