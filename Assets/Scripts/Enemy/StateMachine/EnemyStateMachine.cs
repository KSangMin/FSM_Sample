using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotateDamping { get; private set; }
    public float MovementSpeedModifier { get; set; }

    public GameObject Target {  get; private set; }

    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        MovementSpeed = enemy.Data.GroundData.BaseSpeed;
        RotateDamping = enemy.Data.GroundData.BaseRotationDamping;

        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
    }
}
