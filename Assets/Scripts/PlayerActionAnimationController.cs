using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionAnimationController : MonoBehaviour
{
    private Animator _animator;

    public bool _locked = false;
    
    private int _comboStep = 0;
    [SerializeField] private float _timer;

    public bool Locked
    {
        set { _locked = value; }
    }

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
            PlayUnlocked("WalkR");
        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            PlayUnlocked("Idle");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_comboStep == 0)
            {
                PlayLocked("LightJabR");
                resetTimer();
                _comboStep++;
            }
            else if (_comboStep == 1)
            {
                PlayLocked("CrossPunchR");
                resetTimer();
                _comboStep++;
            }
            else if (_comboStep == 2)
            {
                _comboStep = 0;
                resetTimer();
                PlayLocked("JumpAndGroundSlamR");
            }
        }
    }
    
    public void AnimationFinishedCallback()
    {
        UnlockAnimationController();
    }
    
    void PlayLocked(string stateName)
    {
        if (_locked) return;
        LockAnimationController();
        _animator.Play(stateName);
    }
    
    void PlayUnlocked(string stateName)
    {
        if (_locked) return;
        UnlockAnimationController();
        _animator.Play(stateName);
    }
    
    public void LockAnimationController()
    {
        _locked = true;
    }
    
    public void UnlockAnimationController()
    {
        _locked = false;
    }
    
    public void resetTimer()
    {
        _timer = 1f;
    }
}
