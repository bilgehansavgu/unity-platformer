using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public float moveSpeed = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            TakeDamage(20);
        }
        
    }
    void Update()
    {
        if (currentHealth < maxHealth)
        {
            Move();
        }
    }    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void Move()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
