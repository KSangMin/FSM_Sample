using UnityEngine;

public class AttackState : PlayerBaseState
{
    public AttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;

        base.Enter();

        StartAnimation(stateMachine.Player.animationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.AttackParameterHash);
    }
}
