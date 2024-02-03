using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAnimationController : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsCrossPunch = Animator.StringToHash("isCrossPunch");
    private static readonly int IsPull = Animator.StringToHash("isPull");

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.SetTrigger(IsCrossPunch);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _animator.SetTrigger(IsPull);
        }
    }
}
