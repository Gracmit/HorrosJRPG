using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var start = new StartBattle();
        var enemyChoose = new EnemyChoose();
        var enemyAttack = new EnemyAttack();
        var playerChoose = new PlayerChoose();
        var playerAttack = new PlayerAttack();
        var win = new Win();
        var lose = new Lose();
        
        _stateMachine.AddState(start);
        _stateMachine.AddState(enemyChoose);
        _stateMachine.AddState(enemyAttack);
        _stateMachine.AddState(playerChoose);
        _stateMachine.AddState(playerAttack);
        _stateMachine.AddState(win);
        _stateMachine.AddState(lose);
        
        _stateMachine.AddTransition(playerAttack, win, () => BattleManager.Instance.EnemyCount <= 0);
        _stateMachine.AddTransition(playerAttack, lose, () => BattleManager.Instance.PartyCount <= 0);
        _stateMachine.AddTransition(enemyAttack, lose, () => BattleManager.Instance.PartyCount <= 0);
        _stateMachine.AddTransition(enemyAttack, win, () => BattleManager.Instance.EnemyCount <= 0);
        
        _stateMachine.AddTransition(start, playerChoose, () => BattleManager.Instance.Initialized() && BattleManager.Instance.ActiveEntity.GetType() == typeof(PartyMember));
        _stateMachine.AddTransition(start, enemyChoose, () => BattleManager.Instance.Initialized() && BattleManager.Instance.ActiveEntity.GetType() == typeof(CombatEnemy));
        _stateMachine.AddTransition(playerChoose, playerAttack, () => BattleManager.Instance.AttackHandler.Target != null);
        _stateMachine.AddTransition(enemyChoose, enemyAttack, () => BattleManager.Instance.AttackHandler.Target != null);
        _stateMachine.AddTransition(
            playerAttack,
            playerChoose,
            () => BattleManager.Instance.TurnManager.NextTurn().GetType() == typeof(PartyMember) &&  BattleManager.Instance.AttackHandler.Attacked);
        _stateMachine.AddTransition(
            playerAttack,
            enemyChoose,
            () => BattleManager.Instance.TurnManager.NextTurn().GetType() == typeof(CombatEnemy) &&  BattleManager.Instance.AttackHandler.Attacked);

        _stateMachine.AddTransition(
            enemyAttack,
            enemyChoose,
            () => BattleManager.Instance.TurnManager.NextTurn().GetType() == typeof(CombatEnemy) &&  BattleManager.Instance.AttackHandler.Attacked);
        _stateMachine.AddTransition(
            enemyAttack,
            playerChoose,
            () => BattleManager.Instance.TurnManager.NextTurn().GetType() == typeof(PartyMember) &&  BattleManager.Instance.AttackHandler.Attacked);

        
        _stateMachine.SetState(start);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}