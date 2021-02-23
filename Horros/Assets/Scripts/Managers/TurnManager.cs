﻿using System.Collections.Generic;

public class TurnManager
{
    private Queue<ICombatEntity> _turnQueue = new Queue<ICombatEntity>();
    private Queue<ICombatEntity> _deadEntities = new Queue<ICombatEntity>();


    public ICombatEntity NextTurn
    {
        get
        {
            if (!_turnQueue.Peek().Alive)
            {
                var entity = _turnQueue.Dequeue();
                _deadEntities.Enqueue(entity);
            }

            return _turnQueue.Peek();
        }
    }

    public void ChangeToNextTurn()
    {
        if (BattleManager.Instance.ActiveEntity != null)
            _turnQueue.Enqueue(BattleManager.Instance.ActiveEntity);
        
        BattleManager.Instance.SetActiveEntity(_turnQueue.Dequeue());
        foreach (var entity in _deadEntities)
        {
            if (entity.GetType() == typeof(PartyMember))
            {
                _turnQueue.Enqueue(entity);
            }
        }
    }

    public void AddEntity(ICombatEntity entity)
    {
        _turnQueue.Enqueue(entity);
    }

    public ICombatEntity GetNextEntity()
    {
        return _turnQueue.Dequeue();
    }
}