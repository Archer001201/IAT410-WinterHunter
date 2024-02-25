using System;
using DataSO;
using Enemy;
using UnityEngine;

namespace Snowman.Skills
{
    public class SnowmanExplosion : MonoBehaviour
    {
        private float _attack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy")) other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_attack);
        }

        public void SetAttack(float attack)
        {
            _attack = attack;
        }
    }
}
