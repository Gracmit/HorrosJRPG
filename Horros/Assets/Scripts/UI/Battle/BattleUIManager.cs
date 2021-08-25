using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _actionList;
    [SerializeField] private SkillList _skillList;
    [SerializeField] private StatusList _statusList;
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private ItemList _itemList;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _runAwayScreen;
    [SerializeField] private CollectedItemsText _lootText;
    [SerializeField] private Sprite _nullIcon;
    private static BattleUIManager _instance;
    private Highlighter _highlighter;
    private BattleEventSystemHandler _eventHandler;
    private BattleUIStackHandler _stackHandler;

    public static BattleUIManager Instance => _instance;
    public BattleEventSystemHandler EventHandler => _eventHandler;
    public BattleUIStackHandler StackHandler => _stackHandler;

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

        _highlighter = new Highlighter(_infoPanel);
        _eventHandler = GetComponent<BattleEventSystemHandler>();
        _stackHandler = new BattleUIStackHandler();
    }


    private void Update()
    {
        _highlighter.Tick();

        if (PlayerInput.Instance.GetKeyDown(KeyCode.Q))
        {
            ReturnToPreviousUIObject();
        }
    }

    private void ReturnToPreviousUIObject()
    {
        var x = _stackHandler.GetLastUIObject();
        var activeObject = _stackHandler.GetLastUIObject();
        if (activeObject == null)
        {
            return;
        }

        x.SetActive(false);
        activeObject.SetActive(true);
        _stackHandler.PushToStack(activeObject);
        _highlighter.TurnHighlighterOff();
    }

    public void AddEnemyToHighlighter(ICombatEntity entity) => _highlighter.AddEnemy(entity);

    public void HighlightEntity()
    {
        _highlighter.CanHighlight();
        _actionList.SetActive(false);
        _skillList.gameObject.SetActive(false);
        _itemList.gameObject.SetActive(false);
    }

    public void HighlightAll()
    {
        _highlighter.CanHighlightAll();
        _actionList.SetActive(false);
        _skillList.gameObject.SetActive(false);
        _itemList.gameObject.SetActive(false);
    }

    public void ToggleActionList(bool active)
    {
        _actionList.SetActive(active);
        if (_actionList.activeInHierarchy)
        {
            _stackHandler.PushToStack(_actionList);
        }
    }

    public void ToggleSkillList(bool active)
    {
        _skillList.gameObject.SetActive(active);
        if (_skillList.gameObject.activeInHierarchy)
        {
            _stackHandler.PushToStack(_skillList.gameObject);
        }
    }

    public void ToggleItemsList(bool active)
    {
        _itemList.gameObject.SetActive(active);
        if (_itemList.gameObject.activeInHierarchy)
        {
            _stackHandler.PushToStack(_itemList.gameObject);
        }
    }

    public void ToggleStatusPanels(bool active) => _statusList.gameObject.SetActive(active);

    public void RemoveEnemyFromHighlighter(CombatEnemy enemy) => _highlighter.RemoveEnemy(enemy);

    public void InstantiateSkillButtons(List<Skill> skills) => _skillList.InstantiateButtons(skills);

    public void InstantiateStatusPanel(PartyMember member) => _statusList.InstantiateStatusPanel(member);

    public void InstantiateItemButtons(List<Item> items) => _itemList.InstantiateButtons(items);
    public void UpdateStatusPanel(PartyMember member) => _statusList.UpdatePanel(member);

    public void ToggleVictoryScreen(bool active) => _victoryScreen.SetActive(active);

    public void ToggleDefeatScreen(bool active) => _defeatScreen.SetActive(active);

    public void ToggleRunAwayScreen(bool active) => _runAwayScreen.SetActive(active);

    public void ShowLootedLoot(List<Item> loot) => _lootText.SetText(loot);

    public Sprite GetNoneStatusIcon() => _nullIcon;
}