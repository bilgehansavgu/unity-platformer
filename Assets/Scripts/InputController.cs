using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Animator _animator;
    private int _comboStep = 0;
    private float _lastActionTime = 0f;
    private readonly float _comboResetTime = 10f;
    private bool _comboInProgress = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_comboInProgress)
        {
            if (Time.time - _lastActionTime > _comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    // MOVE EVENT
    public void OnMovement()
    {
        PlayState("WalkR");
        ResetCombo();
    }
    
    // ATTACKS
    bool CheckComboStep(string expectedState)
    {
        switch (_comboStep)
        {
            case 0:
                return expectedState == "CrossPunchR";
            case 1:
                return expectedState == "LightJabR";
            case 2:
                return expectedState == "CrossPunchR";
            case 3:
                return expectedState == "JumpAndGroundSlamR";
            case 4:
                return expectedState == "CrossPunchR";
            case 5:
                return expectedState == "chain_punch_R_animation";
            default:
                return false;
        }
    }
    public void OnCrossPunch()
    {
        if (CheckComboStep("CrossPunchR"))
        {
            PlayState("CrossPunchR");
        }
        else
        {
            ResetCombo();
            _comboInProgress = false;
        }
    }
    public void OnLightJab()
    {
        if (CheckComboStep("LightJabR"))
        {
            PlayState("LightJabR");
        }
        else
        {
            ResetCombo();
            _comboInProgress = false;
        }
    }
    public void OnChainPunch()
    {
        if (CheckComboStep("chain_punch_R_animation"))
        {
            PlayState("chain_punch_R_animation");
        }
        else
        {
            ResetCombo();
            _comboInProgress = false;
        }
    }
    public void OnSlam()
    {
        if (CheckComboStep("JumpAndGroundSlamR"))
        {
            PlayState("JumpAndGroundSlamR");
        }
        else
        {
            ResetCombo();
            _comboInProgress = false;
        }
    }

    // COMBO ANIMATION CONTROLLER
    public void ComboStepAnimationFinishedCallback()
    {
        _comboStep++;
        PlayState("idle");
    }

    public void LastComboStepAnimationFinishedCallback()
    {
        ResetCombo();
        PlayState("idle");
    }
    void PlayState(string stateName)
    {
        _animator.Play(stateName);
        _comboInProgress = true;
        _lastActionTime = Time.time;
    }
    void ResetCombo()
    {
        _comboStep = 0;
        _comboInProgress = false;
        PlayState("idle"); 
    }
}
