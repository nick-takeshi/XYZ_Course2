using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOff : MonoBehaviour
{
    public void SetLight(float intensity)
    {
        var light = GetComponent<Light2D>();
        light.intensity = intensity;
    }

    
}
