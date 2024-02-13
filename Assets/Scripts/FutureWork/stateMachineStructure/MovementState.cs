using UnityEngine;

public class MovementState : MonoBehaviour, IPlayerState
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    public PlayerStateInputs inputHandler;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = FindObjectOfType<PlayerStateInputs>();
    }

    public void EnterState()
    {
        // Initialize state
    }

    public void UpdateState()
    {
        if (inputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found!");
            return;
        }
        Vector2 moveInput = inputHandler.MoveInputValue;
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void ExitState()
    {
        // Clean up state
    }
}