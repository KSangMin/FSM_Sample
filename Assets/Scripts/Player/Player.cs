using UnityEngine;

public class Player : MonoBehaviour
{
    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData animationData {  get; private set; }

    public Animator animator {  get; private set; }
    public PlayerController input {  get; private set; }
    public CharacterController controller { get; private set; }

    private void Awake()
    {
        animationData.Init();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }
}
