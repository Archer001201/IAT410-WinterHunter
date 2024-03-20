using Enemy;
using UnityEngine;

namespace Snowball
{
    /*
     * Throwing snowball
     */
    public class ThrowingSnowball : BaseSnowball
    {
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
