using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    void OnMovement();
    void OnCrossPunch();
    void OnLightJab();
}