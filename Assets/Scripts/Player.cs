using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    private AudioSource jumpSound;
    
    private int jumpCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        jumpSound = GetComponent<AudioSource>();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,10);
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
            rb.velocity = new Vector2(-10,rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(-3,rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = (new Vector2(10,rb.velocity.y));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = (new Vector2(3,rb.velocity.y));
        }
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(new Vector2(-1,0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(new Vector2(-1,0));
        }
    }
}
