using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class SpearLunge : MonoBehaviour
    {
        public BaseEnemy enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerAttribute>().TakeDamage(enemy.attackDamage * 2);
            }

            if (other.CompareTag("Snowman"))
            {
                other.GetComponent<BaseSnowman>().TakeDamage(enemy.attackDamage * 2);
            }
        }
    }
}
