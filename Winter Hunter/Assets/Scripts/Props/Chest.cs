using UnityEngine;

namespace Props
{
    /*
     * Store item data in the chest and handle player interaction
     */
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

        /*
         * Open chest and destroy this game object
         */
        public virtual void OpenChest()
        {
            Destroy(gameObject);
        }
    }
}
