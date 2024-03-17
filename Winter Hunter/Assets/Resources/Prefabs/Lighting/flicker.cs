using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    private Light myLight;
    private float targetIntensity;
    private float changeSpeed = 2f; // Controls how quickly the intensity changes to the target intensity

    void Start()
    {
        myLight = GetComponent<Light>();
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        // Smoothly transition to the target intensity with a dynamic speed
        myLight.intensity = Mathf.Lerp(myLight.intensity, targetIntensity, changeSpeed * Time.deltaTime);

        // Check if the light has reached close to the target intensity
        if (Mathf.Abs(myLight.intensity - targetIntensity) < 0.05f)
        {
            // Change target intensity to a new random value
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            // Optionally, adjust changeSpeed for varied flickering effects
            changeSpeed = Random.Range(20f, 40f); // Adjust these values to get the desired rapidity and smoothness
        }
    }
}


