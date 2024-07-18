using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputs : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction attackSquareAction;
    public InputAction attackTriangleAction;
    public InputAction dashAction;

    public InputActionReference jumpActionReference;

    public Vector2 MoveInputValue;
    
    public bool IsHorizontalMoveInput => MoveInputValue.x != 0;
    
    public bool IsJumpInput = false;
    public bool IsDashInput = false;
    public bool AttackSquareActionTriggered = false;
    public bool AttackTriangleActionTriggered = false;

    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction("Move");
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction("Jump");
        attackSquareAction = playerControls.FindActionMap(actionMapName).FindAction("AttackSquare");
        attackTriangleAction = playerControls.FindActionMap(actionMapName).FindAction("AttackTriangle");
        dashAction = playerControls.FindActionMap(actionMapName).FindAction("Dash");

        SubscribeInputActions();
    }

    void SubscribeInputActions()
    {
        moveAction.performed += context => MoveInputValue = context.ReadValue<Vector2>();;
        moveAction.canceled += context => MoveInputValue = Vector2.zero;

        jumpAction.performed += context => IsJumpInput = true;
        jumpAction.canceled += context => IsJumpInput = false;

        attackSquareAction.performed += context => AttackSquareActionTriggered = true;
        attackSquareAction.canceled += context => AttackSquareActionTriggered = false;

        attackTriangleAction.performed += context => AttackTriangleActionTriggered = true;
        attackTriangleAction.canceled += context => AttackTriangleActionTriggered = false;
        
        dashAction.performed += context => IsDashInput = true;
        dashAction.canceled += context => IsDashInput = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackSquareAction.Enable();
        attackTriangleAction.Enable();
        dashAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackSquareAction.Disable();
        attackTriangleAction.Disable();
        dashAction.Disable();
    }
}
