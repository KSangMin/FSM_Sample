public interface IState
{
    void Enter();
    void Exit();
    void Update();
    void PhysicsUpdate();
    void HandleInput();
}

public abstract class StateMachine
{
    protected IState curState;

    public void ChangeState(IState state)
    {
        curState?.Exit();
        curState = state;
        curState?.Enter();
    }

    public void HabndleInput()
    {
        curState?.Update();
    }

    public void Update()
    {
        curState?.Update();
    }

    public void PhysicsUpdate()
    {
        curState?.PhysicsUpdate();
    }
}
