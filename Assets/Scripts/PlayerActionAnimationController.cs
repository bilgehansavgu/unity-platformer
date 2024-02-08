using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionAnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private bool _locked = false;
    
    [SerializeField] private int _comboStep = 0;
    [SerializeField] private float _timer;
    
    [SerializeField] private bool _isMoving = false;

    private Rigidbody2D rb;
    
    //BURAYA ACIL STATE MACHINE LAZIM HER TARAF IF ELSE IF ELSE IF ELSE
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (rb.velocity.x != 0 && rb.velocity.y != 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        // Timer
        Debug.Log(_comboStep);
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _comboStep = 0;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            PlayInterruptible("WalkR");
        }
        else if ((Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D) == false) || (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A) == false))
        {
            PlayInterruptible("idle");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayLocked("dash_R_animation");
        }

        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) )
        {
            if (_comboStep == 0 && Input.GetKeyDown(KeyCode.N))
            {
                
                PlayLocked("CrossPunchR");
                resetTimer();
            }
            else if (_comboStep == 1 && Input.GetKeyDown(KeyCode.N))
            {
                PlayLocked("LightJabR");
                resetTimer();
            }
            else if (_comboStep == 2 && Input.GetKeyDown(KeyCode.N))
            {
                resetTimer();
                PlayLocked("chain_punch_R_animation");
            }
            else if (_comboStep == 2 && Input.GetKeyDown(KeyCode.M))
            {
                resetTimer();
                PlayLocked("JumpAndGroundSlamR");
            }
        }
    }
    
    public void ComboStepAnimationFinishedCallback()
    {
        _locked = false;
        
        if (_isMoving)
        {
            PlayInterruptible("WalkR");
        }
        else
        {
            PlayInterruptible("idle");
        }

        _comboStep++;
    }
    
    public void LastComboStepAnimationFinishedCallback()
    {
        _locked = false;
                
        if (_isMoving)
        {
            PlayInterruptible("WalkR");
        }
        else
        {
            PlayInterruptible("idle");
        }

        _comboStep = 0;
    }
    
    public void DashAnimationFinishedCallback()
    {
        _locked = false;
                
        if (_isMoving)
        {
            PlayInterruptible("WalkR");
        }
        else
        {
            PlayInterruptible("idle");
        }
    }
    
    public void AnimationFinishedCallback()
    {
        _locked = false;
        PlayInterruptible("idle");
    }
    
    void PlayLocked(string stateName)
    {
        if (_locked) return;
        _locked = true;
        _animator.Play(stateName);
    }
    
    void PlayInterruptible(string stateName)
    {
        if (_locked) return;
        _locked = false;
        _animator.Play(stateName);
    }
    
    // Helpers
    
    public void UnlockAnimationController()
    {
        _locked = false;
    }
    
    public void resetTimer()
    {
        _timer = 1f;
    }
}
