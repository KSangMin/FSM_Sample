using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : GroundState
{
    public RunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        
        base.Enter();

        StartAnimation(stateMachine.Player.animationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.RunParameterHash);
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);

        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
