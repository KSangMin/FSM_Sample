using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : GroundState
{
    public WalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        
        base.Enter();

        StartAnimation(stateMachine.Player.animationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.WalkParameterHash);
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);

        stateMachine.ChangeState(stateMachine.RunState);
    }
}
