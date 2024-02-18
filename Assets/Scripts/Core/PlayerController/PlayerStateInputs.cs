using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.CharacterController
{
    public class PlayerStateInputs : MonoBehaviour
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


        public Vector2 MoveInputValue;
        public bool jumpTriggered = false;
        public bool attackSquareActionTriggered = false;
        public bool attackTriangleActionTriggered = false;
        public bool inputDirection;
        public bool dashTriggered = false;


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
            inputDirection = MoveInputValue.x > 0;
        }

        void SubscribeInputActions()
        {
            moveAction.performed += context => { MoveInputValue = context.ReadValue<Vector2>(); };
            moveAction.canceled += context => { MoveInputValue = Vector2.zero; };

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
}