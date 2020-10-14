using NUnit.Framework;
using UnityEngine;

public class stats : MonoBehaviour
{
    [Test]
    public void can_add()
    {
        Stats stats = new Stats();
        stats.Add(StatType.Attack, 3);
        Assert.AreEqual(3, stats.Get(StatType.Attack));
        stats.Add(StatType.Attack, 5);
        Assert.AreEqual(8, stats.Get(StatType.Attack));
        stats.Add(StatType.Defence, 3);
        Assert.AreEqual(3, stats.Get(StatType.Defence));
    }
    
    
    [Test]
    public void can_remove()
    {
        Stats stats = new Stats();
        stats.Add(StatType.Attack, 3);
        stats.Add(StatType.Defence, 3);
        stats.Remove(StatType.Attack, 2);
        stats.Remove(StatType.Defence, 1);
        Assert.AreEqual(1, stats.Get(StatType.Attack));
        Assert.AreEqual(2, stats.Get(StatType.Defence));
    }
}
