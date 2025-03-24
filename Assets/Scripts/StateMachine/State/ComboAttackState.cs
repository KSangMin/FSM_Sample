using UnityEngine;

public class ComboAttackState : AttackState
{
    private bool alreadyAppliedCombo;
    private bool alreadyApplyForce;

    AttackInfoData attackInfoData;

    public ComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.animationData.ComboAttackParameterHash);

        alreadyAppliedCombo = false;
        alreadyApplyForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        stateMachine.Player.animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.animationData.ComboAttackParameterHash);

        if (!alreadyAppliedCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.animator, "Attack");
        if(normalizedTime < 1)//실행 중(1 이전)
        {
            if (normalizedTime >= attackInfoData.ForceTransitiontime)
            {
                TryApplyForce();
            }

            if (normalizedTime >= attackInfoData.ComboTransitiontime)
            {
                TryComboAttack();
            }
        }
        else
        {
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;
        if (attackInfoData.ComboStateIndex == -1) return;
        if (!stateMachine.IsAttacking) return;

        alreadyAppliedCombo = true;
    }

    void TryApplyForce()
    {
        if(alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.Player.forceReceiver.Reset();
        stateMachine.Player.forceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }
}
