using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField] private ComboSystem comboSystem; 
    [SerializeField] private AnimationController animationController;
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
    private bool isMoving = false;
    private bool isJumping = false;
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
        isMoving = true;
        PlayAnimation("walk_R_animation");
        WalkRight();
    }
    public void OnMoveLeft()
    {
        Debug.Log("Move Left Pressed");
        isMoving = true;
        PlayAnimation("walk_R_animation");
        WalkLeft();
    }
    public void OnDash()
    {
        Debug.Log("Dash Pressed");
        isMoving = true;
        PlayAnimation("dash_R_animation");
    }
    public void OnJump()
    {
        Debug.Log("Jump Pressed");
        isJumping = true;
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
    public void OnEndJump()
    {
        Debug.Log("Jump Ended");
        isJumping = false;
    }
    void Update()
    {
        if (rb.velocity.magnitude < 2f)
        {
            Debug.Log("Player is idle.");
            PlayAnimation("idle");
        }
    }
    private void PlayAnimation(string animationName)
    {
        if (animationController != null)
        {
            animationController.PlayAnimation(animationName);
        }
        else
        {
            Debug.LogWarning("AnimationController reference is null in ComboSystem. Cannot play animation.");
        }
    }
}