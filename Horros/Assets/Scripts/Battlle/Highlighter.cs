using System.Collections.Generic;
using UnityEngine;

public class Highlighter
{
    private List<CombatEnemy> _enemies = new List<CombatEnemy>();
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
            BattleManager.Instance.ActiveMember.AttackHandler.SaveTarget(_enemies[_activeIndex]);
            BattleUIManager.Instance.ToggleSkillList(false);
            _canHighlight = false;
            _infoPanel.gameObject.SetActive(false);
        }
    }

    public void AddEnemy(CombatEnemy enemy)
    {
        _enemies.Add(enemy);
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
        if (_activeIndex == 0)
        {
            _activeIndex = _enemies.Count - 1;
        }
        else
        {
            _activeIndex--;
        }
        Highlight();
    }
    
    private void NextEnemy()
    {
        if (_activeIndex == _enemies.Count - 1)
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
        _enemies[_activeIndex].Highlight();
        _infoPanel.UpdateText(_enemies[_activeIndex]);
    }

    public void RemoveEnemy(CombatEnemy enemy)
    {
        _enemies.Remove(enemy);
        if (_activeIndex >= _enemies.Count)
            _activeIndex--;
    }
}