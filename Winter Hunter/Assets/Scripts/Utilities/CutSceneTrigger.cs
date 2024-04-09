using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class CutSceneTrigger : MonoBehaviour
    {
        public UnityEvent triggerEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            triggerEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
