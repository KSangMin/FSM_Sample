using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }

    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }
    public ForceReceiver forceReceiver { get; private set; }

    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        animationData.Init();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
