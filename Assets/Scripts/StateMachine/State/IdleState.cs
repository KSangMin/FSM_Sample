using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : GroundState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.MovementSpeedModifier = 0;
        StartAnimation(stateMachine.Player.animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);

        stateMachine.Player.animator.SetBool(stateMachine.Player.animationData.RunParameterHash, true);
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        base.OnRunCanceled(context);

        stateMachine.Player.animator.SetBool(stateMachine.Player.animationData.RunParameterHash, false);
    }
}
