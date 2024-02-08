using System;
using UnityEngine;

namespace Props
{
    public class Chest : MonoBehaviour
    {
        public bool canOpen;
        public GameObject unlockingVFX;
        public GameObject hud;

        private void Awake()
        {
            hud.SetActive(false);
        }

        private void Update()
        {
            unlockingVFX.SetActive(!canOpen);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) hud.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) hud.SetActive(false);
        }

        public virtual void PickUp()
        {
            Destroy(gameObject);
        }
    }
}
