using UnityEngine;

public class SprintState : MonoBehaviour, IPlayerState
{

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
    }

    public void UpdateState()
    {
        // You can add any specific logic for the JumpState update if needed
    }

    public void ExitState()
    {
        // You can add any cleanup logic for when the player exits the JumpState
    }
}