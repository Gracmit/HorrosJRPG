using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TurnManager
{
    private List<ICombatEntity> _entities = new List<ICombatEntity>();
    private bool _attacked;

    public bool Attacked => _attacked;

    public void AddEntity(ICombatEntity entity)
    {
        _entities.Add(entity);
    }

    public void SortEntities()
    {
        _entities.Sort(CompareSpeed);
    }

    private int CompareSpeed(ICombatEntity x, ICombatEntity y)
    {
        var xSpeed = x.Data.Stats.GetValue(StatType.Speed);
        var ySpeed = y.Data.Stats.GetValue(StatType.Speed);

        if (xSpeed > ySpeed)
            return 1;

        if (xSpeed < ySpeed)
            return -1;
        
        return Random.Range(-1, 2);
    }

    public void Attack()
    {
        foreach (var entity in _entities)
        {
            entity.Attack();
        }

        _attacked = true;
    }

    public void ResetAttacks()
    {
        _attacked = false;
    }
}