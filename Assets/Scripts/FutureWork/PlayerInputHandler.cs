using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;
    
    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";
    
    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string crossPunch = "CrossPunch";
    [SerializeField] private string lightJab = "LightJab";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction attackCrossPunch;
    private InputAction attackLightJab;

    public Vector2 MoveInput { get; private set;} 
    public bool JumpTriggered { get; private set;}
    public float SprintValue { get; private set;}
    public bool CrossPunchTriggered { get; private set;}
    public bool LightJabTriggered { get; private set;}
    public static PlayerInputHandler Instance { get; private set;}
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        
        attackCrossPunch = playerControls.FindActionMap(actionMapName).FindAction(crossPunch);
        attackLightJab = playerControls.FindActionMap(actionMapName).FindAction(lightJab);
        
        RegisterInputActions();
    }
    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        
        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;       
        
        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;  
        
        attackCrossPunch.performed += context => CrossPunchTriggered = true;
        attackCrossPunch.canceled += context => CrossPunchTriggered = false;   
        
        attackLightJab.performed += context => LightJabTriggered = true;
        attackLightJab.canceled += context => LightJabTriggered = false;   
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        
        attackCrossPunch.Enable();
        attackLightJab.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        
        attackCrossPunch.Disable();
        attackLightJab.Disable();
    }
}
