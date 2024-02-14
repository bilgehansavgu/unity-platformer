using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementSystem
{
    event System.Action OnAnimationFinished;
    void WalkRight();
    void WalkLeft();
    void Jump();
}