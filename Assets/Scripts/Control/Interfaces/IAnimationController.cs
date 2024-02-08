using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationController
{
    void PlayAnimation(string animationName);
    float GetAnimationLength(string animationName);
}