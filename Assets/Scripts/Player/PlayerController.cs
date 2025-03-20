using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerInputs {  get; private set; }
    public PlayerInputActions.PlayerActions playerActions { get; private set; }

    private void Start()
    {
        playerInputs = GetComponent<PlayerInputActions>();
        playerActions = playerInputs.Player;
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
