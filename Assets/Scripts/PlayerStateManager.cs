using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;
    
    void Start()
    {
        ChangeState(new IdleState());
    }
    
    void Update()
    {
        currentState.UpdateState(this);

        switch (currentState)
        {
            case IdleState:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(new WalkingState());
                }
                if (Input.GetKey(KeyCode.W))
                {
                    ChangeState(new JumpingState());
                }
                break;
            
            case WalkingState:
                if (Input.GetKey(KeyCode.W))
                {
                    ChangeState(new JumpingState());
                }
                break;
            
            case JumpingState:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(new WalkingState());
                }
                if (Input.GetKeyDown(KeyCode.W) && GetComponent<PlayerParam>().GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    ChangeState(new DoubleJumpingState());
                }
                if (GetComponent<PlayerParam>().GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    ChangeState(new IdleState());
                }
                break;
            
            case DoubleJumpingState:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(new WalkingState());
                }
                if (GetComponent<PlayerParam>().GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    ChangeState(new IdleState());
                }
                break;
        }
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        newState.EnterState(this);
        currentState = newState;
    }
}