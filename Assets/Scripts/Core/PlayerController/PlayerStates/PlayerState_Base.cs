﻿using UnityEngine;

public abstract class PlayerState_Base : StateBase<PlayerController.StateID>
{
    protected PlayerController parent;

    protected PlayerState_Base(PlayerController parent)
    {
        this.parent = parent;
    }
    protected void PlayClip(string clipName)
    {
        parent.Animator.Play(clipName);
    }

    protected void PlayClip(string clipName, float index, int totalFramesInAnimation)
    {
        parent.Animator.Play(clipName, 0, (1f / totalFramesInAnimation) * index);
    }
}