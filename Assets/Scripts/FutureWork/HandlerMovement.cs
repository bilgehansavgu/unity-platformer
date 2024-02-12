using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private float horizontalInput;
    private bool isGrounded;
    private bool shouldJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }
    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;
        shouldJump = inputHandler.JumpTriggered && isGrounded;
        if (horizontalInput != 0)
        {
            FlipSprite(horizontalInput);
        }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        
        if (shouldJump)
        {
            ApplyJump();
        }
    }

    void ApplyMovement()
    {
        float speed = moveSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
   
    void ApplyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        shouldJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) ;
        {
            isGrounded = true;
        }
    }
    private void FlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
