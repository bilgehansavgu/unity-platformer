using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField] private ComboSystem comboSystem; 
    [SerializeField]private float _maxMovementVelocity = 10;
    [SerializeField]private float _movementVelocityAfterMovement = 2; 
    [SerializeField]private float _dashVelocity = 5;
    [SerializeField]private float _jumpVelocity = 5;
    [SerializeField]private float _jumpFallVelDecrement = 3f;
    [SerializeField]private float _jumpRiseVelDec = 3f;
    [SerializeField]private float _jumpRiseVelDecHold = 1f;
    private float lastPunchTime;
    private float punchInterval = 0.3f;
    private bool isFaceRight = true;
    private bool isJumping = true;
    private Rigidbody2D rb;
    private CapsuleCollider2D _collider;
    public IComboSystem ComboSystem 
    { 
        get { return comboSystem; } 
        set { comboSystem = value as ComboSystem; } 
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player GameObject.");
        }
    }
    public void OnCrossPunch()
    {
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Cross Punch Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnCrossPunch();
        }
    }
    public void OnLightJab()
    {
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Light Jab Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnLightJab();
        }
    }
    public void OnMoveRight()
    {
        Debug.Log("Move Right Pressed");
        WalkRight();
    }
    public void OnMoveLeft()
    {
        Debug.Log("Move Left Pressed");
        WalkLeft();
    }
    public void OnJump()
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
    private void WalkRight()
    {
        if (rb.velocity.x < _maxMovementVelocity)
        {
            float speedDifference = Mathf.Abs(_maxMovementVelocity - rb.velocity.x);
            rb.velocity += new Vector2(speedDifference, 0);
        }    
    }
    private void WalkLeft()
    {
        if (rb.velocity.x > -_maxMovementVelocity)
        {
            float speedDifference = Mathf.Abs(_maxMovementVelocity + rb.velocity.x);
            rb.velocity += new Vector2(-speedDifference, 0);
        }    
    }
}