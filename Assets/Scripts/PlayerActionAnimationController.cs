using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionAnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("isCrossPunch");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("isPull");
        }
    }
}
