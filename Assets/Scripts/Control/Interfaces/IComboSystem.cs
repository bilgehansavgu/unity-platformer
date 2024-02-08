using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComboSystem
{
    event System.Action OnAnimationFinished;
    void OnCrossPunch();
    void OnLightJab();
    
}