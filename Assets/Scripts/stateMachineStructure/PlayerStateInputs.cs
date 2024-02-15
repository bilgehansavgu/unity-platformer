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
    public bool jumpTriggered = false;
    public bool attackSquareActionTriggered = false;
    public bool attackTriangleActionTriggered = false;
    public bool inputDirection;

    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        attackSquareAction = playerControls.FindActionMap(actionMapName).FindAction(attackSquare);
        attackTriangleAction = playerControls.FindActionMap(actionMapName).FindAction(attackTriangle);

        SubscribeInputActions();
    }

    private void Update()
    {
        inputDirection = MoveInputValue.x > 0;
    }

    void SubscribeInputActions()
    {
        moveAction.performed += context => {
            MoveInputValue = context.ReadValue<Vector2>();
        };
        moveAction.canceled += context =>
        {
            MoveInputValue = Vector2.zero;
        };

        jumpAction.performed += context => jumpTriggered = true;
        jumpAction.canceled += context => jumpTriggered = false;

        attackSquareAction.performed += context => attackSquareActionTriggered = true;
        attackSquareAction.canceled += context => attackSquareActionTriggered = false;
            
        attackTriangleAction.performed += context => attackTriangleActionTriggered = true;
        attackTriangleAction.canceled += context => attackTriangleActionTriggered = false;
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
