using System;
using Player;
using Snowball;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class Smash : MonoBehaviour
    {
        public ParticleSystem smashVfx;
        private float _attack;

        private void Update()
        {
            if (smashVfx.isStopped)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerAttribute>().health -= _attack;
            }

            if (other.CompareTag("Snowman"))
            {
                other.GetComponent<BaseSnowman>().health -= _attack;
            }
        }

        public void SetAttack(float attack)
        {
            _attack = attack;
        }
    }
}