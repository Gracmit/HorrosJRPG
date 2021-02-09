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
        
        _stateMachine.AddTransition(start, playerChoose, BattleManager.Instance.Initialized);
        
        _stateMachine.SetState(start);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}