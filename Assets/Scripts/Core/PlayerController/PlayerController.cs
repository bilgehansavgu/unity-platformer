using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public PlayerConfig config;
    [Header("References & Setup")]
    public Animator Animator;
    public Rigidbody2D Rb;
    public PlayerInputs PlayerInputs;
    public BoxCollider2D BoxCollider2D;
    
    
    private float originalFriction;
    
    [SerializeField, Space] StateMachine<StateID> fsm;

    public enum StateID
    {
        Idle,
        Move,
        Jump,
        Falling,
        Landing,
        SquareAttack,
        TriangleAttack,
        Dash
    }
    
    private void SetupFSM()
    {
        Dictionary<StateID, IState<StateID>> states = new Dictionary<StateID, IState<StateID>>
        {
            { StateID.Idle, new PlayerState_Idle(this) },
            { StateID.Move, new PlayerState_Move(this) },
            { StateID.Jump, new PlayerState_Jump(this) },
            { StateID.Falling, new PlayerState_Falling(this) },
            { StateID.Landing, new PlayerState_Landing(this) },
            { StateID.SquareAttack, new PlayerState_SquareAttack(this) },
            { StateID.TriangleAttack, new PlayerState_TriangleAttack(this)},
            { StateID.Dash, new PlayerState_Dash(this)}
        };
        fsm = new StateMachine<StateID>(states, StateID.Idle);
    }
    
    private void Awake()
    {
        if (config == null)
        {
            Debug.Log("Player config is missing. PlayerController is disabled");
            this.enabled = false;
        }
        GetReferences();
        SetupFSM();
        
        // originalFriction = BoxCollider2D.sharedMaterial.friction;
    }

    private void GetReferences()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();
        if (Rb == null)
            Rb = GetComponent<Rigidbody2D>();
        if (PlayerInputs == null)
            PlayerInputs = GetComponent<PlayerInputs>();
        if (BoxCollider2D == null)
            BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        fsm.UpdateState();
        
        // if (IsGrounded())
        // {
        //     // If the player is grounded, set the friction back to the original value
        //     BoxCollider2D.sharedMaterial.friction = originalFriction;
        // }
        // else
        // {
        //     // If the player is not grounded, set the friction to 0
        //     BoxCollider2D.sharedMaterial.friction = 0;
        // }
    }
  
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, config.GroundCheckDistance, config.GroundLayer);
        return hit.collider != null && !hit.collider.isTrigger;
    }
    
    public void InvokeState(StateID stateToTrigger)
    {
        fsm.GetState(stateToTrigger).InvokeState(fsm);
    }
    
    public float GetAirSprite(int totalFramesInAnimation) // (11,-11 are paramaters to tune. Not fixed!
    {
        return Map(Rb.velocity.y, 12f, -11f, 0, totalFramesInAnimation-1) ;
    }
    
    private float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
