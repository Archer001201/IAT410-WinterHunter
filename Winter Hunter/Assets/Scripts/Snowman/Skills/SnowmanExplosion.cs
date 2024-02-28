using System;
using DataSO;
using Enemy;
using UnityEngine;
using Utilities;

namespace Snowman.Skills
{
    public class SnowmanExplosion : MonoBehaviour
    {
        private float _attack;
        private ShieldBreakEfficiency _shieldBreakEfficiency;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;
            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_attack, _shieldBreakEfficiency);
        }

        public void SetAttack(float attack, ShieldBreakEfficiency shieldBreakEfficiency)
        {
            _attack = attack;
            _shieldBreakEfficiency = shieldBreakEfficiency;
        }
    }
}
