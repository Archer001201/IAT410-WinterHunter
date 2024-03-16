using System;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Snowman.Skills
{
    public class SnowmanSlash : MonoBehaviour
    {
        public float attackDuration = 1f;
        private float _rotationSpeed;
        public BaseEnemy enemyAttr;

        private void Awake()
        {
            _rotationSpeed = 360.0f / attackDuration;
        }

        private void Update()
        {
            var rotationThisFrame = _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationThisFrame, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerAttribute>().TakeDamage(enemyAttr.attackDamage);
            }

            if (other.CompareTag("Snowman"))
            {
                other.gameObject.GetComponent<BaseSnowman>().TakeDamage(enemyAttr.attackDamage);
            }
        }
    }
}
