using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collider;
    
    private int jumpCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
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
                rb.velocity += new Vector2(0,5);
                jumpCount -= 1; 
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
    }
}
