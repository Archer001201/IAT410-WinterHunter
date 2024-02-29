using System;
using UnityEngine;

namespace Utilities
{
    public class Teleport : MonoBehaviour
    {
        public string nextLevel;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            EventHandler.OpenTeleportPanel(true, nextLevel);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            EventHandler.OpenTeleportPanel(false, string.Empty);
        }
    }
}
