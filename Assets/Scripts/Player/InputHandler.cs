using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerInputActions playerInputs {  get; private set; }
    public PlayerInputActions.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
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
