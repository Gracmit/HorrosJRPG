using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private EnemyRoaming _roamer;
    public EnemyRoaming Roamer => _roamer;

    void Awake()
    {
        _stateMachine = new StateMachine();
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var player = FindObjectOfType<PlayerMovementController>().gameObject;
        _roamer = new EnemyRoaming(navMeshAgent, player);
        var roam = new Roam(_roamer);
        var follow = new Follow(_roamer);
        
        _stateMachine.AddState(roam);
        _stateMachine.AddState(follow);
        
        _stateMachine.AddTransition(roam, follow, () => _roamer.LookForPlayer());
        _stateMachine.AddTransition(follow, roam, () => _roamer.LostTarget);
        
        _stateMachine.SetState(roam);
    }

    
    void Update()
    {
        _stateMachine.Tick();
    }
}