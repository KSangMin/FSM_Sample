using UnityEngine;

public class JumpState : AirState
{
    public JumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.forceReceiver.Jump(stateMachine.JumpForce);

        base.Enter();
        Debug.Log("Movement: " + stateMachine.Player.forceReceiver.Movement);

        StartAnimation(stateMachine.Player.animationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.JumpParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.controller.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
