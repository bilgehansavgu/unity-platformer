using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateInputs : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string attackSquare = "AttackSquare";
    [SerializeField] private string attackTriangle = "AttackTriangle";

    public InputAction moveAction ;
    public InputAction jumpAction;
    public InputAction sprintAction;
    public InputAction attackSquareAction;
    public InputAction attackTriangleAction;
    
    public Vector2 MoveInputValue { get; private set; }



    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        attackSquareAction = playerControls.FindActionMap(actionMapName).FindAction(attackSquare);
        attackTriangleAction = playerControls.FindActionMap(actionMapName).FindAction(attackTriangle);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => {
            MoveInputValue = context.ReadValue<Vector2>();
            
            Debug.Log("moveActionperformed" + MoveInputValue);
        };
        moveAction.canceled += context =>
        {
            MoveInputValue = Vector2.zero;
            Debug.Log("moveActioncanceled" + MoveInputValue);
        };
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        attackSquareAction.Enable();
        attackTriangleAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        attackSquareAction.Disable();
        attackTriangleAction.Disable();
    }
}
