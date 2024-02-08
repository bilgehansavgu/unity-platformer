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
    public float GetAnimationLength(string animationName)
    {
        AnimationClip clip = FindAnimationClip(animationName);

        if (clip != null)
        {
            return clip.length;
        }
        else
        {
            Debug.LogWarning("Animation clip '" + animationName + "' not found.");
            return 0f;
        }
    }

    private AnimationClip FindAnimationClip(string animationName)
    {
        if (animator != null)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == animationName)
                {
                    return clip;
                }
            }
        }
        return null;
    }
}