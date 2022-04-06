public class Follow : IState
{
    private readonly EnemyRoaming _roamer;

    public Follow(EnemyRoaming roamer)
    {
        _roamer = roamer;
    }

    public void Tick()
    {
        _roamer.FollowPlayer();
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        _roamer.NegateLostTarget();
    }
}