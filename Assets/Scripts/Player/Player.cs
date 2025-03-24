using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data {  get; private set; }

    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData animationData {  get; private set; }

    public Animator animator {  get; private set; }
    public InputHandler input {  get; private set; }
    public CharacterController controller { get; private set; }
    public ForceReceiver forceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    public Health health { get; private set; }

    private void Awake()
    {
        animationData.Init();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<InputHandler>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();
        health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        stateMachine.ChangeState(stateMachine.IdleState);
        health.OnDead += OnDead;
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

    void OnDead()
    {
        animator.SetTrigger("Dead");
        enabled = false;
    }
}
