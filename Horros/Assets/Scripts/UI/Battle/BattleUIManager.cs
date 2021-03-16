using System;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _actionList;
    private static BattleUIManager _instance;
    private Highlighter _highlighter;

    public static BattleUIManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }

        _highlighter = new Highlighter();
    }

    private void Update()
    {
        _highlighter.Tick();
    }

    public void AddEnemy(CombatEnemy entity)
    {
        _highlighter.AddEnemy(entity);
    }

    public void HighlightEnemy()
    {
        _highlighter.CanHighlight();
    }

    public void ToggleActionList(bool active)
    {
        _actionList.SetActive(active);
    }

    public void RemoveEnemyFromHighlighter(CombatEnemy enemy)
    {
        _highlighter.RemoveEnemy(enemy);

    }
}
