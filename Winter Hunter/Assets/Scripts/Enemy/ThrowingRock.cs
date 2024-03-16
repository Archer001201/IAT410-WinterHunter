using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class ThrowingRock : MonoBehaviour
    {
        private float _attack;
        public bool isLanded;

        private void OnCollisionEnter(Collision other)
        {
            if (isLanded) return;
            
            if (other.gameObject.CompareTag("Ground"))
            {
                isLanded = true;
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
