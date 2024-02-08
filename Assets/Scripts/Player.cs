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

    [SerializeField]private float _maxMovementVelocity = 10;
    [SerializeField]private float _movementVelocityAfterMovement = 2; 
    [SerializeField]private float _dashVelocity = 5;
    
    [SerializeField]private float _jumpVelocity = 5;
    [SerializeField]private float _jumpFallVelDecrement = 3f;
    [SerializeField]private float _jumpRiseVelDec = 3f;
    [SerializeField]private float _jumpRiseVelDecHold = 1f;
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

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (_jumpCount > 0)
            {
                rb.velocity += new Vector2(0, -rb.velocity.y);
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                
                _jumpCount -= 1; 
                _jumpSound.Play();
            }
           
            Debug.Log(_jumpCount);
        }
        if (_collider.gameObject.CompareTag("Floor"))
        {
            _jumpCount = 2;
        }
        
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            if (_isFaceRight)
            {
                rb.AddForce(new Vector2(7,0), ForceMode2D.Impulse);
            } else
            {
                rb.AddForce(new Vector2(-7,0), ForceMode2D.Impulse);
            }
        }

        //Walk Left
        if (Input.GetKey(KeyCode.A))
        {
            // Flip
            if (_isFaceRight)
            {
                transform.Rotate(0,180,0);
                _isFaceRight = false;
            }
            WalkLeft();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity += new Vector2(-rb.velocity.x/2, 0);
        }
        
        //Walk Right
        if (Input.GetKey(KeyCode.D))
        {
            // Flip
            if (!_isFaceRight)
            {
                transform.Rotate(0,180,0);
                _isFaceRight = true;
            }
            WalkRight();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity += new Vector2(-rb.velocity.x/2, 0);
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

    private void FixedUpdate()
    {
        // Jump
        
        // if falling down
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpFallVelDecrement * Time.deltaTime);
        }
        
        // if rising and space hold down
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
        }
        
        // if rising but space not hold down
        else if (rb.velocity.y > 0 && Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDecHold * Time.deltaTime);
        }
    }
}
