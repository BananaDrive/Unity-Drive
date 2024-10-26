using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light lightObject;   
    public float Off = 0f;
    public float On = 1f;
    private bool isOn = false;

    void Update()
    {
  
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLightIntensity();
        }
    }

    void ToggleLightIntensity()
    {
        if (isOn)
        {
            lightObject.intensity = Off;   
        }
        else
        {
            lightObject.intensity = On;  
        }

        isOn = !isOn; 
    }
}