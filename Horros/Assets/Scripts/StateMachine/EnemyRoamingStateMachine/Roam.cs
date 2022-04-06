using UnityEngine;

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
        _roamer.SetNewPoint();
        Debug.Log("Roaming");
    }

    public void OnExit()
    {
    }
}