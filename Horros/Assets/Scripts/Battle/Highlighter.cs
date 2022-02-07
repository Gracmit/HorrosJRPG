using System.Collections.Generic;
using UnityEngine;

public class Highlighter
{
    private List<ICombatEntity> _entities = new List<ICombatEntity>();
    private List<ICombatEntity> _highlightedGroup = new List<ICombatEntity>();
    private int _activeIndex = 0;
    private bool _canHighlight;
    private bool _highlightAll;
    private InfoPanel _infoPanel;

    public Highlighter(InfoPanel infoPanel)
    {
        _infoPanel = infoPanel;
    }

    public void Tick()
    {
        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.A))
        {
            if (_highlightAll)
                ChangeHighlightedGroup();
            else
                PreviousEnemy();
        }

        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.D))
        {
            if (_highlightAll)
                ChangeHighlightedGroup();
            else
                NextEnemy();
        }

        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.Space))
        {
            if (_highlightAll)
                BattleManager.Instance.ActiveMember.AttackHandler.SaveTargets(_highlightedGroup);
            else
                BattleManager.Instance.ActiveMember.AttackHandler.SaveTargets(_entities[_activeIndex]);

            BattleUIManager.Instance.ToggleSkillList(false);
            _entities[_activeIndex].UnHighlight();
            _canHighlight = false;
            _highlightAll = false;
            _infoPanel.gameObject.SetActive(false);
        }

        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.Q))
        {
            _entities[_activeIndex].UnHighlight();
            _canHighlight = false;
        }
    }

    private void ChangeHighlightedGroup()
    {
        UnHighlightAll();
        if (_highlightedGroup[0].GetType() == typeof(CombatEnemy))
        {
            _highlightedGroup.Clear();
            foreach (var entity in _entities)
            {
                if (entity.GetType() == typeof(PartyMember))
                    _highlightedGroup.Add(entity);
            }
        }
        else
        {
            _highlightedGroup.Clear();
            foreach (var entity in _entities)
            {
                if (entity.GetType() == typeof(CombatEnemy))
                    _highlightedGroup.Add(entity);
            }
        }

        HighlightAll();
    }


    public void AddEnemy(ICombatEntity entity)
    {
        _entities.Add(entity);
    }

    public void CanHighlight()
    {
        _canHighlight = true;
        _infoPanel.gameObject.SetActive(true);
        BattleUIManager.Instance.StackHandler.PushToStack(_infoPanel.gameObject);
        Highlight();
    }

    public void CanHighlightAll()
    {
        _highlightedGroup.Clear();
        foreach (var entity in _entities)
        {
            if (entity.GetType() == typeof(CombatEnemy))
                _highlightedGroup.Add(entity);
        }

        _canHighlight = true;
        _highlightAll = true;
        HighlightAll();
    }


    public void TurnHighlighterOff()
    {
        _canHighlight = false;
        _highlightAll = false;
    }

    private void PreviousEnemy()
    {
        _entities[_activeIndex].UnHighlight();
        if (_activeIndex == 0)
        {
            _activeIndex = _entities.Count - 1;
        }
        else
        {
            _activeIndex--;
        }

        Highlight();
    }

    private void NextEnemy()
    {
        _entities[_activeIndex].UnHighlight();
        if (_activeIndex == _entities.Count - 1)
        {
            _activeIndex = 0;
        }
        else
        {
            _activeIndex++;
        }

        Highlight();
    }

    private void Highlight()
    {
        BattleCameraManager.Instance.SetTarget(_entities[_activeIndex]);
        _entities[_activeIndex].Highlight();
        Debug.Log($"Highlighted: {_entities[_activeIndex].Data.Name}");
        _infoPanel.UpdatePanel(_entities[_activeIndex]);
    }

    private void HighlightAll()
    {
        foreach (var entity in _highlightedGroup)
        {
            entity.Highlight();
        }
    }

    private void UnHighlightAll()
    {
        foreach (var entity in _highlightedGroup)
        {
            entity.UnHighlight();
        }
    }


    public void RemoveEnemy(CombatEnemy enemy)
    {
        _entities.Remove(enemy);
        if (_activeIndex >= _entities.Count)
            _activeIndex--;
    }
}