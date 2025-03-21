using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data {  get; private set; }

    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData animationData {  get; private set; }

    public Animator animator {  get; private set; }
    public PlayerController input {  get; private set; }
    public CharacterController controller { get; private set; }
    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        animationData.Init();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
