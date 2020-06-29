using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class ChangeBrightness : MonoBehaviour
{
    private void Start()
    {
        UpdateBrightness();  
    }
public void UpdateBrightness()
    {
        Light2D light = GetComponent<Light2D>();
        light.intensity = PlayerPrefs.GetFloat("Brightness");
      
    }
}
