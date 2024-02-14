using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour, IMovementSystem
{
    [SerializeField] private AnimationController animationController;
    [SerializeField]private float _maxMovementVelocity = 10;
    [SerializeField]private float _movementVelocityAfterMovement = 2; 
    [SerializeField]private float _dashVelocity = 5;
    [SerializeField]private float _jumpVelocity = 5;
    [SerializeField]private float _jumpFallVelDecrement = 3f;
    [SerializeField]private float _jumpRiseVelDec = 3f;
    [SerializeField]private float _jumpRiseVelDecHold = 1f;
    private bool _isFaceRight = true;
    public event System.Action OnAnimationFinished;
    private Rigidbody2D rb;
    public float speed = 5f; // Speed of player movement
    private Vector2 moveInput;
    public bool IsMoving { get; private set;}
    public float walkSpeed = 5f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump Pressed");
        // if falling down
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpFallVelDecrement * Time.deltaTime);
        }
    
        // if rising and space hold down
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
        }
    
        // if rising but space not hold down
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDecHold * Time.deltaTime);
        }
    }
    public void Jump()
    {
        Debug.Log("Jump Pressed");
        // if falling down
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpFallVelDecrement * Time.deltaTime);
        }
    
        // if rising and space hold down
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
        }
    
        // if rising but space not hold down
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDecHold * Time.deltaTime);
        }
    }
    public void WalkRight()
    {
        // Flip
        if (!_isFaceRight)
        {
            transform.Rotate(0,180,0);
            _isFaceRight = true;
        }
        if (rb.velocity.x < _maxMovementVelocity)
        {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity - rb.velocity.x);
            rb.velocity += new Vector2(speedDifference, 0);
        }
    }

    public void WalkLeft()
    {
        // Flip
        if (_isFaceRight)
        {
            transform.Rotate(0,180,0);
            _isFaceRight = false;
        }
        if (rb.velocity.x > -_maxMovementVelocity)
        {
            float speedDifference = Mathf.Abs(_maxMovementVelocity + rb.velocity.x);
            rb.velocity += new Vector2(-speedDifference, 0);
        }    
    }
 

}
