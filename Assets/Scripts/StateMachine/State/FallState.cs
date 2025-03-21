using UnityEngine;

public class FallState : AirState
{
    public FallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.animationData.FallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.FallParameterHash);
    }
}
