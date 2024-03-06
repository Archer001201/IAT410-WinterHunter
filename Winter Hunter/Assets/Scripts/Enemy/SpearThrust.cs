using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class SpearThrust : MonoBehaviour
    {
        public ParticleSystem thrustVfx;
        public BaseEnemy enemy;

        private void OnEnable()
        {
            // if (enemy is InfantrySoldier infantrySoldier)
            //     infantrySoldier.isThrustStopped = false;
            thrustVfx.Play();
        }

        private void Update()
        {
            if (thrustVfx.isStopped)
            {
                // if (enemy is InfantrySoldier infantrySoldier)
                //     infantrySoldier.isThrustStopped = true;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerAttribute>().TakeDamage(enemy.attackDamage);
            }
            else if (other.CompareTag("Snowman"))
            {
                other.gameObject.GetComponent<BaseSnowman>().TakeDamage(enemy.attackDamage);
            }
        }
    }
}
