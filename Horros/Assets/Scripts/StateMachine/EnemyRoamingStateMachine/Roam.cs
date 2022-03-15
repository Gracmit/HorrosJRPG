using UnityEngine.AI;

public class Roam : IState
{
    private readonly EnemyRoaming _roamer;

    public Roam(EnemyRoaming roamer)
    {
        _roamer = roamer;
    }
    

    public void Tick()
    {
        _roamer.Roam();
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}