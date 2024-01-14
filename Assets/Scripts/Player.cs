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

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        jumpSound = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
                rb.velocity += new Vector2(0,5);
                jumpCount -= 1; 
                jumpSound.Play();
            }
           
            Debug.Log(jumpCount);
        }
        if (collider.gameObject.CompareTag("Floor"))
        {
            jumpCount = 2;
            Debug.Log("Collided with floor.");
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-3,0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(+3,0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(new Vector2(-1,0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(new Vector2(-1,0));
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
