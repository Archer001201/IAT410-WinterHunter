using System;
using Player;
using Snowman;
using UnityEngine;

namespace Enemy
{
    public class FireBall : MonoBehaviour
    {
        public float speed;

        private Vector3 _direction;
        private float _attack;
        
        private void Update()
        {
            transform.Translate(_direction * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            if (otherGO.CompareTag("Enemy") || otherGO.CompareTag("Projectile")) return;
            
            if (otherGO.CompareTag("Player"))
            {
                otherGO.GetComponent<PlayerAttribute>().TakeDamage(_attack);
            }

            if (otherGO.CompareTag("Snowman"))
            {
                otherGO.GetComponent<BaseSnowman>().TakeDamage(_attack);
            }
            
            Destroy(gameObject);
        }

        public void SetFireBall(Vector3 direction, float attack)
        {
            _direction = direction;
            _attack = attack;
        }
    }
}
