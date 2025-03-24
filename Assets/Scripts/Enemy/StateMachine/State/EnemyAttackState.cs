using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyApplyForce;
    private bool alreadyAppliedDealing;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Enemy.animationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.animationData.BaseAttackParameterHash);

        alreadyApplyForce = false;
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Enemy.animationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.animationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.animator, "Attack");
        if (normalizedTime < 1)//실행 중(1 이전)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitiontime)
            {
                TryApplyForce();
            }

            if(!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.DealingStartTransitiontime)
            {
                stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if(alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.DealingEndTransitiontime)
            {
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if(!IsInChasingRange()) stateMachine.ChangeState(stateMachine.ChasingState);
            else stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.Enemy.forceReceiver.Reset();
        stateMachine.Enemy.forceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);
    }
}
