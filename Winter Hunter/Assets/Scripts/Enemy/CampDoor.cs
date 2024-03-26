using System;
using UnityEngine;

namespace Enemy
{
    public class CampDoor : MonoBehaviour
    {
        public EnemyCamp enemyCamp;
        public GameObject vfx;

        private void Awake()
        {
            vfx.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (enemyCamp.isPlayerInCamp) return;
                enemyCamp.NotifyEnemiesToChangeChasingState();
                enemyCamp.isPlayerInCamp = true;
            }
        }
    }
}
