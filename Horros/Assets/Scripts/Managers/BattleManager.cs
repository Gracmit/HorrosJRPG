using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _playerSpawnpoints;
    [SerializeField] private List<Transform> _enemySpawnpoints;
    [SerializeField] private List<CinemachineVirtualCamera> _chooseCameras;
    private static BattleManager _instance;
    private TurnManager _turnManager;
    private PartyMember _activeMember;
    private bool _initializationCompleted;
    private bool _enemiesReady;
    private int _enemyCount;
    private int _partyCount;
    private readonly List<PartyMember> _party = new List<PartyMember>();
    private readonly List<CombatEnemy> _enemies = new List<CombatEnemy>();
    private int _partyIndex;
    private bool _partyReady;

    public static BattleManager Instance => _instance;
    public PartyMember ActiveMember => _activeMember;
    public int EnemyCount => _enemyCount;
    public int PartyCount => _partyCount;
    public TurnManager TurnManager => _turnManager;
    public List<PartyMember> Party => _party;
    public List<CombatEnemy> Enemies => _enemies;
    public bool EnemiesReady => _enemiesReady;

    public bool PartyReady => _partyReady;

    public bool Initialized() => _initializationCompleted;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _turnManager = new TurnManager();
    }

    public void InitializeBattleField()
    {
        var statusData = StatusManager.Instance.StatusData;
        InstantiateEnemies(statusData);
        InstantiatePartyMembers();
        _partyIndex = 0;
        _initializationCompleted = true;
    }


    private void InstantiatePartyMembers()
    {
        var spawnpointCounter = 0;
        foreach (var entity in GameManager.Instance.ActiveParty)
        {
            var model = Instantiate(entity.Model, _playerSpawnpoints[spawnpointCounter]);
            entity.CombatAvatar = model;
            entity.FullHeal();
            entity.Alive = true;
            entity.SetAttackHandler();
            entity.SetRenderer();

            var chooseCamera = _chooseCameras[spawnpointCounter];
            chooseCamera.gameObject.SetActive(false);
            entity.SetCameras(chooseCamera);

            spawnpointCounter++;
            _turnManager.AddEntity(entity);
            _party.Add(entity);
            BattleUIManager.Instance.InstantiateStatusPanel(entity);
            BattleUIManager.Instance.AddEnemyToHighlighter(entity);
        }

        _partyCount = _party.Count;
    }

    private void InstantiateEnemies(StatusData statusData)
    {
        var spawnpointCounter = 0;
        foreach (var entityData in statusData.enemyGroup)
        {
            var enemy = new CombatEnemy(entityData);
            var model = Instantiate(entityData.Model, _enemySpawnpoints[spawnpointCounter]);
            enemy.CombatAvatar = model;
            enemy.SetRenderer();
            enemy.FullHeal();
            spawnpointCounter++;
            enemy.SetAttackHandler();
            _enemies.Add(enemy);
            _turnManager.AddEntity(enemy);
            BattleUIManager.Instance.AddEnemyToHighlighter(enemy);
        }

        _enemyCount = statusData.enemyGroup.Count;
    }

    public void SetActiveEntity()
    {
        if (!_party[_partyIndex].Alive)
        {
            while (
                _partyIndex != _party.Count &&
                !_party[_partyIndex].Alive) // if there is PartyMembers left on the list and next partyMember in not alive
            {
                _partyIndex++;
            }
        }
        _activeMember = _party[_partyIndex];
        BattleCameraManager.Instance.SetCamera(_activeMember.ChooseCamera);
        Debug.Log(_activeMember.Data.Name);
        _partyIndex++;

        while (
            _partyIndex != _party.Count &&
            !_party[_partyIndex].Alive) // if there is PartyMembers left on the list and next partyMember in not alive
        {
            _partyIndex++;
        }

        if (_partyIndex == _party.Count) // if reached the end of the partyMemberList
            _partyReady = true;
    }

    public void ResetPartyIndex()
    {
        _partyIndex = 0;
        _partyReady = false;
    }

    public void SaveChosenAttack(Skill skill) => _activeMember.AttackHandler.SaveAttack(skill);
    public void SaveChosenItem(Consumable item) => _activeMember.AttackHandler.SaveItem(item);

    public void EnemyDied(CombatEnemy enemy)
    {
        _enemyCount--;
        BattleUIManager.Instance.RemoveEnemyFromHighlighter(enemy);
    }

    public void ChooseEnemyAttacks()
    {
        StartCoroutine(Wait());
        foreach (var enemy in _enemies)
        {
            enemy.PrepareAttack();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _enemiesReady = true;
    }

    public void EnemiesNotReady() => _enemiesReady = false;

    public void PartyMemberDied()
    {
        _partyCount--;
    }

    public void PartyMemberRevived()
    {
        _partyCount++;
    }

    public void GiveLoot()
    {
        var inventory = FindObjectOfType<Inventory>();
        foreach (var enemy in _enemies)
        {
            var enemyLoot = enemy.EnemyData.Loot;
            foreach (var loot in enemyLoot)
            {
                inventory.PickUpItem(loot);
            }
        }
    }

}