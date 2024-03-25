using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class FireRay : MonoBehaviour
    {
        public FlameRays rays;
        private float _attack;

        // private void Awake()
        // {
        //     _attack = rays.GetAttack();
        // }

        private void Start()
        {
            _attack = rays.GetAttack();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerAttribute>().TakeDamage(_attack);
            }

            if (other.CompareTag("Snowman"))
            {
                other.GetComponent<BaseSnowman>().TakeDamage(_attack);
            }
        }
    }
}