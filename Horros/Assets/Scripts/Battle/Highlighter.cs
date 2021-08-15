using System.Collections.Generic;
using UnityEngine;

public class Highlighter
{
    private List<ICombatEntity> _entities = new List<ICombatEntity>();
    private int _activeIndex = 0;
    private bool _canHighlight;
    private InfoPanel _infoPanel;

    public Highlighter(InfoPanel infoPanel)
    {
        _infoPanel = infoPanel;
    }

    public void Tick()
    {
        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.A))
        {
            PreviousEnemy();
        }
        
        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.D))
        {
            NextEnemy();
        }

        if (_canHighlight && PlayerInput.Instance.GetKeyDown(KeyCode.Space))
        {
            BattleManager.Instance.ActiveMember.AttackHandler.SaveTarget(_entities[_activeIndex]);
            BattleUIManager.Instance.ToggleSkillList(false);
            _entities[_activeIndex].UnHighlight();
            _canHighlight = false;
            _infoPanel.gameObject.SetActive(false);
        }
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

    public void TurnHighlighterOff()
    {
        _canHighlight = false;
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

    public void RemoveEnemy(CombatEnemy enemy)
    {
        _entities.Remove(enemy);
        if (_activeIndex >= _entities.Count)
            _activeIndex--;
    }
}