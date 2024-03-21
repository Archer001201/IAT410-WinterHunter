using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public PlayerController playerController;
        private bool _canThrow;
        private bool _canPrint;

        private void Awake()
        {
            _canThrow = true;
            _canPrint = true;
        }

        public void EnableAttack()
        {
            playerController.canAttack = true;
            _canThrow = true;
        }

        public void ThrowSnowball()
        {
            if (!_canThrow) return;
            playerController.ThrowingSnowballAttack();
            _canThrow = false;
        }

        public void RollSnowball()
        {
            playerController.CreateRollingSnowball();
        }

        public void PlayerDies()
        {
            playerController.PlayerDies();
        }

        public void CreateFootprint(GameObject footprint)
        {
            if (!_canPrint) return;
            Instantiate(footprint, transform.position, transform.rotation);
            _canPrint = false;
        }

        public void CanPrint()
        {
            _canPrint = true;
        }
    }
}
