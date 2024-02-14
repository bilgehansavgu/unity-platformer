using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // Layer mask to define what is considered ground
    [SerializeField] private float groundCheckDistance = 1f; // Distance to check for ground

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        // Perform a raycast downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        // If the raycast hit something and it's not a trigger collider, consider the player grounded
        return hit.collider != null && !hit.collider.isTrigger;
    }

    // Add other movement-related methods and logic as needed
}