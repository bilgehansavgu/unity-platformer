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

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
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
        

        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M) )
        {
            if (_comboStep == 0 && Input.GetKeyDown(KeyCode.N))
            {
                PlayLocked("LightJabR");
                resetTimer();
            }
            else if (_comboStep == 1 && Input.GetKeyDown(KeyCode.N))
            {
                PlayLocked("CrossPunchR");
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
        PlayInterruptible("idle");
        _comboStep++;
    }
    
    public void LastComboStepAnimationFinishedCallback()
    {
        _locked = false;
        _comboStep = 0;
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
