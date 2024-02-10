using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{ 
    void OnCrossPunch();
    void OnLightJab();
    void OnMoveRight();
    void OnMoveLeft();
    void OnJump();
    void OnDash();

}