using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class SpearLunge : MonoBehaviour
    {
        public BaseEnemy enemy;

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            if (otherGO.CompareTag("Player"))
            {
                otherGO.GetComponent<PlayerAttribute>().TakeDamage(enemy.attackDamage * 2);
            }

            if (otherGO.CompareTag("Snowman"))
            {
                otherGO.GetComponent<SnowmanTakeDamage>().TakeDamage(enemy.attackDamage * 2);
            }
        }
    }
}
