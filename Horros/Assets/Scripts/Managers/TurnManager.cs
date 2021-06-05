using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TurnManager
{
    private List<ICombatEntity> _entities = new List<ICombatEntity>();
    private bool _attacked;
    private int _activeIndex = 0;

    public bool Attacked => _attacked;
    public ICombatEntity ActiveEntity => _entities[_activeIndex];
    public ICombatEntity NextEntity
    {
        get
        {
            if (_activeIndex + 1 >= _entities.Count)
                return null;
            
            return _entities[_activeIndex + 1];
        }
    }

    public void AddEntity(ICombatEntity entity)
    {
        _entities.Add(entity);
    }

    public void SortEntities()
    {
        if (_activeIndex == 0)
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
        _entities[_activeIndex].Attack();
    }

    public void ResetAttack()
    {
        _entities[_activeIndex].ResetAttack();
    }

    public void NextTurn()
    {
        if (_activeIndex >= _entities.Count - 1)
            _activeIndex = 0;
        else
            _activeIndex++;
    }
}