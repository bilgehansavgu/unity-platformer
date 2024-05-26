using UnityEngine;
using UnityEngine.InputSystem;

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

    public Vector2 MoveInputValue;
    public bool JumpTriggered = false;
    public bool AttackSquareActionTriggered = false;
    public bool AttackTriangleActionTriggered = false;
    public bool DashTriggered = false;

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

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        attackSquareAction.performed += context => AttackSquareActionTriggered = true;
        attackSquareAction.canceled += context => AttackSquareActionTriggered = false;

        attackTriangleAction.performed += context => AttackTriangleActionTriggered = true;
        attackTriangleAction.canceled += context => AttackTriangleActionTriggered = false;
        
        dashAction.performed += context => DashTriggered = true;
        dashAction.canceled += context => DashTriggered = false;
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
