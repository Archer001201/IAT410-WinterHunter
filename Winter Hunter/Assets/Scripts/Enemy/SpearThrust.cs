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
            thrustVfx.Play();
        }

        private void Update()
        {
            if (thrustVfx.isStopped)
            {
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
