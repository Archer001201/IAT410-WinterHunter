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
        public GameObject hud;
        public SnowmanTypeAndLevel snowman;

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
        public void OpenChest()
        {
            EventHandler.OpenSnowmanChest(snowman);
            Destroy(gameObject);
        }
    }
}
