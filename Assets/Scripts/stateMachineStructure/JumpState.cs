using UnityEngine;

public class JumpState : MonoBehaviour, IPlayerState
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnterState()
    {
        // Perform jump action
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void UpdateState()
    {
        // You can add any specific logic for the JumpState update if needed
    }
    public void OnAnimationFinished()
    {
        animator.Play("idle");
    }
    public void ExitState()
    {
        // You can add any cleanup logic for when the player exits the JumpState
    }
}