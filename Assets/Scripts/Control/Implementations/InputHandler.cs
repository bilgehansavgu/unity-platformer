using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField] private ComboSystem comboSystem; 

    public IComboSystem ComboSystem 
    { 
        get { return comboSystem; } 
        set { comboSystem = value as ComboSystem; } 
    }
    public void OnMovement()
    {
        // Implement your movement logic here
    }

    public void OnCrossPunch()
    {
        Debug.Log("Cross Punch Pressed");
        if (ComboSystem != null)
        {
            Debug.Log("Combo system initialized");
            ComboSystem.OnCrossPunch();
        }
    }

    public void OnLightJab()
    {
        Debug.Log("Light Jab Pressed");
        if (ComboSystem != null)
        {
            Debug.Log("Combo system initialized");
            ComboSystem.OnLightJab();
        }
    }
}