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

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction attackSquareAction;
    private InputAction attackTriangleAction;

    public PlayerStateMachine stateMachine;
    public Vector2 MoveInputValue { get; private set; }
    public static PlayerStateInputs Instance { get; private set;}
    private bool inAttackState = false;



    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        attackSquareAction = playerControls.FindActionMap(actionMapName).FindAction(attackSquare);
        attackTriangleAction = playerControls.FindActionMap(actionMapName).FindAction(attackTriangle);
        
        stateMachine = GetComponent<PlayerStateMachine>();

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => {
            if (!inAttackState) // Allow movement only if not in an attack state
            {
                MoveInputValue = context.ReadValue<Vector2>();
                stateMachine.SetState(GetComponent<MovementState>());
            }
        };
        moveAction.canceled += context => MoveInputValue = Vector2.zero;

        jumpAction.performed += context => {
            if (!inAttackState) // Allow jumping only if not in an attack state
            {
                stateMachine.SetState(GetComponent<JumpState>());
            }
        };
        sprintAction.performed += context => {stateMachine.SetState(GetComponent<SprintState>());};

        attackSquareAction.performed += context => {stateMachine.SetState(GetComponent<SquareAttackState>());};

        attackTriangleAction.performed += context => {stateMachine.SetState(GetComponent<TriangleAttackState>());};
    }
    public void SetAttackState(bool value)
    {
        inAttackState = value;
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
