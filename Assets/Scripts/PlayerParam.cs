using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParam : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

}