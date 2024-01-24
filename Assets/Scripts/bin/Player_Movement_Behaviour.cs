using System.Collections;
using UnityEngine;

public class Player_Movement_Behaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    private AudioSource jumpSound;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private float movementVelocity = 10;
    private float movementVelocityAfterMovement = 2;

    private enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        DoubleJumping
    }

    private PlayerState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        jumpSound = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb.mass = 10;

        currentState = PlayerState.Idle;
    }

    void Update()
    {
        HandleInput();
        UpdateState();
    }

    void HandleInput()
    {
        // Handle input based on the current state
        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdleInput();
                break;
            case PlayerState.Walking:
                HandleWalkingInput();
                break;
            case PlayerState.Jumping:
                HandleJumpingInput();
                break;
        }
    }

    void UpdateState()
    {
        // Update state-specific logic
        switch (currentState)
        {
            case PlayerState.Idle:
                Debug.Log("In Idle state");
                // Add logic for the Idle state
                break;
            case PlayerState.Walking:
                Debug.Log("In Walking state");
                // Add logic for the Walking state
                break;
            case PlayerState.Jumping:
                // Add logic for the Jumping state
                //if (collider.gameObject.CompareTag("Floor"))
                //{
                //   currentState = PlayerState.Idle;
            //     Debug.Log("Transitioning to Idle state after jumping");
                // }
                // break;
                if (rb.velocity.y == 0)
                {
                    currentState = PlayerState.Idle;
                    Debug.Log("Transitioning to Idle state after double jumping");
                }
                break;

            case PlayerState.DoubleJumping:
                // Add logic for the DoubleJumping state
                if (rb.velocity.y <= 0)
                {
                    currentState = PlayerState.Idle;
                    Debug.Log("Transitioning to Idle state after double jumping");
                }
                break;
        }
    }

    void HandleIdleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Initial jump logic
            rb.velocity = new Vector2(rb.velocity.x, 5);
            jumpSound.Play();
            currentState = PlayerState.Jumping;
            Debug.Log("Transitioning to Jumping state");
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            currentState = PlayerState.Walking;
            Debug.Log("Transitioning to Walking state");
        }
    }

    void HandleWalkingInput()
    {
        // Handle walking input
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementVelocity, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(movementVelocity, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -movementVelocity);
        }
        else
        {
            currentState = PlayerState.Idle;
            Debug.Log("Transitioning to Idle state");
        }
    }

    void HandleJumpingInput()
    {
        // Handle jumping input
        if (Input.GetKeyDown(KeyCode.W) && currentState == PlayerState.Jumping)
        {
            // Double jump logic
            rb.velocity = new Vector2(rb.velocity.x, 5);
            jumpSound.Play();
            currentState = PlayerState.DoubleJumping;
            Debug.Log("Transitioning to DoubleJumping state");
        }
    }

    void HandleDoubleJumpingInput()
    {
        // Handle double jumping input
        if (Input.GetKeyDown(KeyCode.W) && rb.velocity.y > 0)
        {
            // Double jump logic
            rb.velocity = new Vector2(rb.velocity.x, 5);
            jumpSound.Play();
            currentState = PlayerState.DoubleJumping;
            Debug.Log("Transitioning to DoubleJumping state");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void GainHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }
}
