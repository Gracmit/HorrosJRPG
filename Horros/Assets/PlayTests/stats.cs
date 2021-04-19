﻿using NUnit.Framework;
using UnityEngine;

public class stats : MonoBehaviour
{
    [Test]
    public void can_add()
    {
        Stats stats = ScriptableObject.CreateInstance<Stats>();
        stats.Add(StatType.Attack, 3);
        Assert.AreEqual(3, stats.GetValue(StatType.Attack));
        stats.Add(StatType.Attack, 5);
        Assert.AreEqual(8, stats.GetValue(StatType.Attack));
        stats.Add(StatType.Defence, 3);
        Assert.AreEqual(3, stats.GetValue(StatType.Defence));
    }
    
    
    [Test]
    public void can_remove()
    {
        Stats stats = ScriptableObject.CreateInstance<Stats>();
        stats.Add(StatType.Attack, 3);
        stats.Add(StatType.Defence, 3);
        stats.Subtract(StatType.Attack, 2);
        stats.Subtract(StatType.Defence, 1);
        Assert.AreEqual(1, stats.GetValue(StatType.Attack));
        Assert.AreEqual(2, stats.GetValue(StatType.Defence));
    }
}
