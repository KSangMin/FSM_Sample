using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    protected PlayerController input;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.groundData = stateMachine.Player.Data.GroundData;
        this.input = stateMachine.Player.input;
    }

    public virtual void Enter()
    {
        AddInputActionCallbacks();

        Debug.Log(this.GetType());
    }

    public virtual void Exit()
    {
        RemoveInputActionCallbacks();
    }

    protected virtual void AddInputActionCallbacks()
    {
        input.playerActions.Move.canceled += OnMoveCanceled;
        input.playerActions.Run.started += OnRunStarted;
        input.playerActions.Run.canceled += OnRunCanceled;
    }

    protected virtual void RemoveInputActionCallbacks()
    {
        input.playerActions.Move.canceled -= OnMoveCanceled;
        input.playerActions.Run.started -= OnRunStarted;
        input.playerActions.Run.canceled -= OnRunCanceled;
    }

    public virtual void HandleInput()
    {
        ReadMoveInput();
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.animator.SetBool(animatorHash, false);
    }

    private void ReadMoveInput()
    {
        stateMachine.MovementInput = stateMachine.Player.input.playerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 dir = GetMoveDir();

        Move(dir);

        Rotate(dir);
    }

    private Vector3 GetMoveDir()
    {
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 dir)
    {
        float moveSpeed = GetMovementSpeed();
        stateMachine.Player.controller.Move((dir * moveSpeed) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        return stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
    }

    private void Rotate(Vector3 dir)
    {
        if(dir != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotateDamping * Time.deltaTime);
        }
    }
}
