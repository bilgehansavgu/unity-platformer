using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D _collider;
    private AudioSource jumpSound;
    
    private int jumpCount;

    private float movementVelocity = 10;
    private float movementVelocityAfterMovement = 2;
    
    private bool isFaceRight = true;

    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
        if (other.collider.tag == "Wall")
        {
            //transform.rotation = Quaternion.Euler(0,0,90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0)
            {
                //rb.velocity = new Vector2(rb.velocity.x,10);
                rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                jumpCount -= 1; 
                jumpSound.Play();
            }
           
            Debug.Log(jumpCount);
        }
        if (_collider.gameObject.CompareTag("Floor"))
        {
            jumpCount = 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (isFaceRight)
            {
                transform.Rotate(0,180,0);
                isFaceRight = false;
            }
            rb.velocity = new Vector2(-movementVelocity,rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementVelocityAfterMovement,rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!isFaceRight)
            {
                transform.Rotate(0,180,0);
                isFaceRight = true;
            }
            rb.velocity = (new Vector2(movementVelocity,rb.velocity.y));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = (new Vector2(movementVelocityAfterMovement,rb.velocity.y));
        }
    }

    private void FixedUpdate()
    {
        // JUMP
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (3f) * Time.deltaTime);
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (2f) * Time.deltaTime);
        }
    }
}
