using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.groundData = stateMachine.Enemy.Data.GroundData;
    }

    public virtual void Enter()
    {
        Debug.Log(this.GetType());
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Enemy.animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Enemy.animator.SetBool(animatorHash, false);
    }

    private void Move()
    {
        Vector3 dir = GetMoveDir();

        Move(dir);

        Rotate(dir);
    }

    private Vector3 GetMoveDir()
    {
        return stateMachine.Target.transform.position - stateMachine.Enemy.transform.position;
    }

    private void Move(Vector3 dir)
    {
        float moveSpeed = GetMovementSpeed();
        stateMachine.Enemy.controller.Move((dir * moveSpeed + stateMachine.Enemy.forceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        return stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
    }

    private void Rotate(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Enemy.transform;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotateDamping * Time.deltaTime);
        }
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.controller.Move(stateMachine.Enemy.forceReceiver.Movement * Time.deltaTime);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo curInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && curInfo.IsTag(tag))
        {
            return curInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool IsInChasingRange()
    {
        if (stateMachine.Target.isDead) return false;

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }
}
