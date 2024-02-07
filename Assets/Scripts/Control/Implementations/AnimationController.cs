using UnityEngine;

public class AnimationController : MonoBehaviour, IAnimationController
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(string animationName)
    {
        animator = GetComponent<Animator>();
        animator.Play(animationName);
    }
    public void OnAnimationFinished()
    {
        animator.Play("idle"); 
    }
}