using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _actionList;
    [SerializeField] private SkillList _skillList;
    private StatusList _statusList;
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
        _statusList = GetComponentInChildren<StatusList>();
    }

    private void Update() => _highlighter.Tick();

    public void AddEnemy(CombatEnemy entity) => _highlighter.AddEnemy(entity);

    public void HighlightEnemy() => _highlighter.CanHighlight();

    public void ToggleActionList(bool active) => _actionList.SetActive(active);

    public void ToggleSkillList(bool active) => _skillList.gameObject.SetActive(active);

    public void RemoveEnemyFromHighlighter(CombatEnemy enemy) => _highlighter.RemoveEnemy(enemy);

    public void InstantiateSkillButtons(List<Skill> skills) => _skillList.InstantiateButtons(skills);

    public void InstantiateStatusPanel(PartyMember member) => _statusList.InstantiateStatusPanel(member);
}