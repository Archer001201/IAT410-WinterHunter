using UnityEngine;

namespace Utilities
{
    public class FireController : MonoBehaviour
    {
        public ParticleSystem vfx;
        public GameObject fireLight;

        private void Awake() {
            vfx.Stop();
            fireLight.SetActive(false);
        }
        // private void OnTriggerEnter(Collider other) {
        //     if (other.CompareTag("Player"))
        //     {
        //         vfx.Play();
        //         fireLight.SetActive(true);
        //     }
        // }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player"))
            {
                vfx.Stop();
                fireLight.SetActive(false);
            }
        }

        public void LightFire()
        {
            vfx.Play();
            fireLight.SetActive(true);
        }
    }
}
