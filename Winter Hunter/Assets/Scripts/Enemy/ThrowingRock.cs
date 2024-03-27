using System;
using Cinemachine;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class ThrowingRock : MonoBehaviour
    {
        private float _attack;
        public bool isLanded;
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (isLanded) return;
            
            if (other.gameObject.CompareTag("Ground"))
            {
                isLanded = true;
                _impulseSource.GenerateImpulseWithForce(1f);
            }

            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerAttribute>().TakeDamage(_attack);
            }

            if (other.gameObject.CompareTag("Snowman"))
            {
                other.gameObject.GetComponent<BaseSnowman>().TakeDamage(_attack);
            }
        }

        public void SetAttack(float attack)
        {
            _attack = attack;
        }
    }
}
