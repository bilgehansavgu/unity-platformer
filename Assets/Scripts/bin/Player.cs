using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    private AudioSource jumpSound;
    private int jumpCount;
    
    public int maxHealth = 100;

    public int currentHealth;

    public HealthBar healthBar;

    private float movementVelocity = 10;
    private float movementVelocityAfterMovement = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        jumpSound = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb.mass = 10;
    }

    private void Awake()
    {
        jumpCount = 2;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Floor")
        {
            jumpCount = 2;	
        }
        if (other.collider.tag == "Wall")
        {
            transform.rotation = Quaternion.Euler(0,0,90);
        }
    }
    void Update()
    
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GainHealth(20);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,5);
                jumpCount -= 1; 
                jumpSound.Play();
            }
           
            Debug.Log(jumpCount);
        }
        if (collider.gameObject.CompareTag("Floor"))
        {
            jumpCount = 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementVelocity,rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementVelocityAfterMovement,rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = (new Vector2(movementVelocity,rb.velocity.y));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = (new Vector2(movementVelocityAfterMovement,rb.velocity.y));
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = (new Vector2(rb.velocity.x, movementVelocity));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = (new Vector2(rb.velocity.x, -movementVelocity));
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
}
