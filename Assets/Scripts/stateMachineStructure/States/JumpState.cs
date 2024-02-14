using UnityEngine;

public class JumpState : MonoBehaviour, IPlayerState
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

        rb.velocity += new Vector2(rb.velocity.x, jumpForce);
    }

    public void UpdateState()
    {

    }
    
    public void ExitState()
    {

    }
}