using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var start = new StartBattle();
        var enemyChoose = new EnemyChoose();
        var playerChoose = new PlayerChoose(FindObjectOfType<Inventory>());
        var attack = new Attack();
        var win = new Win();
        var lose = new Lose();
        var runAway = new RunAway();
        
        _stateMachine.AddState(start);
        _stateMachine.AddState(enemyChoose);
        _stateMachine.AddState(playerChoose);
        _stateMachine.AddState(attack);
        _stateMachine.AddState(win);
        _stateMachine.AddState(lose);
        _stateMachine.AddState(runAway);

        _stateMachine.AddTransition(attack, win, () => BattleManager.Instance.EnemyCount <= 0);
        _stateMachine.AddTransition(attack, lose, () => BattleManager.Instance.PartyCount <= 0);
        _stateMachine.AddTransition(start, enemyChoose, () => BattleManager.Instance.Initialized());
        _stateMachine.AddTransition(enemyChoose, playerChoose, () => BattleManager.Instance.EnemiesReady);
        _stateMachine.AddTransition(playerChoose, playerChoose,
            () => BattleManager.Instance.ActiveMember.AttackHandler.AttackChosen && !BattleManager.Instance.PartyReady);
        _stateMachine.AddTransition(playerChoose, attack,
            () => BattleManager.Instance.ActiveMember.AttackHandler.AttackChosen && BattleManager.Instance.PartyReady);
        _stateMachine.AddTransition(
            attack,
            enemyChoose,
            () => BattleManager.Instance.TurnManager.ActiveEntity.Attacked && BattleManager.Instance.TurnManager.NextEntity == null);
        
        _stateMachine.AddTransition(
            attack,
            attack,
            () => BattleManager.Instance.TurnManager.ActiveEntity.Attacked && BattleManager.Instance.TurnManager.NextEntity != null);
        _stateMachine.AddTransition(playerChoose, runAway, () => BattleManager.Instance.RunAway);
        
        _stateMachine.SetState(start);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}