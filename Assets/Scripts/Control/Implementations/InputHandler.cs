using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField] private ComboSystem comboSystem; 
    [SerializeField] private MovementSystem movementSystem; 
    [SerializeField] private AnimationController animationController;
    private float punchInterval = 0.3f;
    private float lastPunchTime;
    public IComboSystem ComboSystem
    { 
        get { return comboSystem; } 
        set { comboSystem = value as ComboSystem; } 
    }
    public IMovementSystem MovementSystem
    { 
        get { return movementSystem; } 
        set { movementSystem = value as MovementSystem; } 
    }
    public void OnCrossPunch()
    {
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Cross Punch Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnCrossPunch();
        }
    }
    public void OnLightJab()
    {
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Light Jab Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnLightJab();
        }
    }
    public void OnMoveRight()
    {
        PlayAnimation("walk_R_animation");
        MovementSystem.WalkRight();
    }
    public void OnMoveLeft()
    {
        PlayAnimation("walk_R_animation");
        MovementSystem.WalkLeft();
    }
    public void OnJump()
    {
        Debug.Log("Jump Pressed");
        MovementSystem.Jump();
    }
    public void OnDash()
    {
        Debug.Log("Dash Pressed");
        PlayAnimation("dash_R_animation");
    }
    private void PlayAnimation(string animationName)
    {
        if (animationController != null)
        {
            animationController.PlayAnimation(animationName);
        }
        else
        {
            Debug.LogWarning("AnimationController reference is null in ComboSystem. Cannot play animation.");
        }
    }
}