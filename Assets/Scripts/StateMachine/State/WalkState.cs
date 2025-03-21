using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : GroundState
{
    public WalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        StartAnimation(stateMachine.Player.animationData.WalkParameterHash);

        if (stateMachine.Player.animator.GetBool(stateMachine.Player.animationData.RunParameterHash) == true)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
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
