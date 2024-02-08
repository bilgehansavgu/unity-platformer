using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputHandler
{
    [SerializeField] private ComboSystem comboSystem; 
    private float lastPunchTime;
    private float punchInterval = 0.3f;

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
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Cross Punch Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnCrossPunch();
        }
    }

    public void OnLightJab()
    {
        float currentTime = Time.time;
        if (currentTime - lastPunchTime >= punchInterval)
        {
            Debug.Log("Light Jab Pressed");
            lastPunchTime = currentTime;
            ComboSystem.OnLightJab();
        }
    }
}