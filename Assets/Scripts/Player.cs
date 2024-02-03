using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D _collider;
    private AudioSource _jumpSound;
    
    private int _jumpCount;

    [SerializeField]private float _movementVelocity = 10;
    [SerializeField]private float _movementVelocityAfterMovement = 2;
    [SerializeField] private float _jumpVelocity = 5;
    
    private bool _isFaceRight = true;
    

    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        _jumpSound = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _jumpCount = 2;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Floor")
        {
            _jumpCount = 2;
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
            if (_jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,_jumpVelocity);
                //rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                _jumpCount -= 1; 
                _jumpSound.Play();
            }
           
            Debug.Log(_jumpCount);
        }
        if (_collider.gameObject.CompareTag("Floor"))
        {
            _jumpCount = 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (_isFaceRight)
            {
                transform.Rotate(0,180,0);
                _isFaceRight = false;
            }
            rb.velocity = new Vector2(-_movementVelocity,rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(-_movementVelocityAfterMovement,rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!_isFaceRight)
            {
                transform.Rotate(0,180,0);
                _isFaceRight = true;
            }
            rb.velocity = (new Vector2(_movementVelocity,rb.velocity.y));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = (new Vector2(_movementVelocityAfterMovement,rb.velocity.y));
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
