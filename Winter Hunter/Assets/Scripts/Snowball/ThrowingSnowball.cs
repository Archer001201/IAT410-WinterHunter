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
                otherGO.GetComponent<BaseEnemy>().TakeDamage(damage, shieldBreakEfficiency);
                PlayerAttr.mana += damage * PlayerAttr.manaRecovery;
            }
            Destroy(gameObject);
        }
    }
}
