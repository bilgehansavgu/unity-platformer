using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    void OnMoveRight();
    void OnMoveLeft();
    void OnJump();
}