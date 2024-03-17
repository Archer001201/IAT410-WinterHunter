using UnityEngine;

namespace Props
{
    public class Flicker : MonoBehaviour
    {
        public float minIntensity = 0.5f;
        public float maxIntensity = 1.5f;
        private Light _myLight;
        private float _targetIntensity;
        private float _changeSpeed = 2f; // Controls how quickly the intensity changes to the target intensity

        private void Awake()
        {
            _myLight = GetComponent<Light>();
            _targetIntensity = Random.Range(minIntensity, maxIntensity);
        }

        private void FixedUpdate()
        {
            // Smoothly transition to the target intensity with a dynamic speed
            _myLight.intensity = Mathf.Lerp(_myLight.intensity, _targetIntensity, _changeSpeed * Time.fixedDeltaTime);

            // Check if the light has reached close to the target intensity
            if (Mathf.Abs(_myLight.intensity - _targetIntensity) < 0.05f)
            {
                // Change target intensity to a new random value
                _targetIntensity = Random.Range(minIntensity, maxIntensity);
                // Optionally, adjust changeSpeed for varied flickering effects
                _changeSpeed = Random.Range(5f, 10f); // Adjust these values to get the desired rapidity and smoothness
            }
        }
    }
}
