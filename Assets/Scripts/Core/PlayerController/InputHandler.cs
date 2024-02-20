using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Core
{
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        [Header("Input Action Asset")] [SerializeField]
        private InputActionAsset playerControls;

        [Header("Action Map Name References")] [SerializeField]
        private string actionMapName = "Player";

        [Header("Action Name References")] [SerializeField]
        private string move = "Move";

        [SerializeField] private string jump = "Jump";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string attackSquare = "AttackSquare";
        [SerializeField] private string attackTriangle = "AttackTriangle";
        [SerializeField] private string dash = "Dash";

        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction attackSquareAction;
        public InputAction attackTriangleAction;
        public InputAction dashAction;


        private Vector2 moveInputValue;
        private bool jumpTriggered = false;
        private bool attackSquareActionTriggered = false;
        private bool attackTriangleActionTriggered = false;
        private bool inputDirection;
        private bool dashTriggered = false;

        public Vector2 MoveInputValue => moveInputValue;

        public bool JumpTriggered => jumpTriggered;

        public bool AttackSquareActionTriggered => attackSquareActionTriggered;

        public bool AttackTriangleActionTriggered => attackTriangleActionTriggered;

        public bool InputDirection => inputDirection;

        public bool DashTriggered => dashTriggered;

        private void Awake()
        {
            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
            attackSquareAction = playerControls.FindActionMap(actionMapName).FindAction(attackSquare);
            attackTriangleAction = playerControls.FindActionMap(actionMapName).FindAction(attackTriangle);
            dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);

            SubscribeInputActions();
        }

        private void Update()
        {
            inputDirection = moveInputValue.x > 0;
        }

        void SubscribeInputActions()
        {
            moveAction.performed += context => { moveInputValue = context.ReadValue<Vector2>(); };
            moveAction.canceled += context => { moveInputValue = Vector2.zero; };

            jumpAction.performed += context => jumpTriggered = true;
            jumpAction.canceled += context => jumpTriggered = false;

            attackSquareAction.performed += context => attackSquareActionTriggered = true;
            attackSquareAction.canceled += context => attackSquareActionTriggered = false;

            attackTriangleAction.performed += context => attackTriangleActionTriggered = true;
            attackTriangleAction.canceled += context => attackTriangleActionTriggered = false;
            
            dashAction.performed += context => dashTriggered = true;
            dashAction.canceled += context => dashTriggered = false;
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
    public interface IInputProvider
    {
        public Vector2 MoveInputValue { get; }
        public bool JumpTriggered { get; }
        public bool AttackSquareActionTriggered { get; }
        public bool AttackTriangleActionTriggered { get; }
        public bool InputDirection { get; }
        public bool DashTriggered { get; }
    }
}