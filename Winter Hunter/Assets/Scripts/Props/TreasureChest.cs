using Snowman;
using UnityEngine;
using Utilities;

namespace Props
{
    /*
     * Store item data in the chest and handle player interaction
     */
    public class TreasureChest : MonoBehaviour
    {
        public bool canOpen;
        public GameObject unlockingVFX;
        public SnowmanTypeAndLevel snowman;

        private void Update()
        {
            unlockingVFX.SetActive(!canOpen);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) EventHandler.ShowInteractableSign(true, "Open");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) EventHandler.ShowInteractableSign(false, "Open");
        }

        /*
         * Open chest and destroy this game object
         */
        public void OpenChest()
        {
            EventHandler.OpenSnowmanChest(snowman);
            EventHandler.ShowInteractableSign(false, "Open");
            Destroy(gameObject);
        }
    }
}
