using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotateDamping {  get; private set; }
    public float MovementSpeedModifier { get; set; }
    public float JumpForce { get; set; }

    public Transform MainCamTransform { get; set; }

    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public RunState RunState { get; private set; }

    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;

        MainCamTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotateDamping = player.Data.GroundData.BaseRotationDamping;

        IdleState = new IdleState(this);
        WalkState = new WalkState(this);
        RunState = new RunState(this);

        JumpState = new JumpState(this);
        FallState = new FallState(this);
    }
}
