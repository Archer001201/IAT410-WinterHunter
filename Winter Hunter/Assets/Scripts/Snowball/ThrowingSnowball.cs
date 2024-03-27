using Cinemachine;
using Enemy;
using UnityEngine;

namespace Snowball
{
    /*
     * Throwing snowball
     */
    public class ThrowingSnowball : BaseSnowball
    {
        private CinemachineImpulseSource _impulseSource;
        
        protected override void Awake()
        {
            base.Awake();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            _impulseSource.GenerateImpulseWithForce(0.25f);
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            if (otherGO.CompareTag("Player")) return;
            if (otherGO.CompareTag("Enemy"))
            {
                var enemy = otherGO.GetComponent<BaseEnemy>();
                if (enemy.isMarked) damage += 30;
                enemy.TakeDamage(damage, shieldBreakEfficiency);
                PlayerAttr.mana += damage * PlayerAttr.manaRecovery;
            }
            Destroy(gameObject);
        }
    }
}
