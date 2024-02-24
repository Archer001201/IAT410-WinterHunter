using Enemy;
using UnityEngine;

namespace Snowball
{
    /*
     * Throwing snowball
     */
    public class ThrowingSnowball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            if (otherGO.CompareTag("Player")) return;
            if (otherGO.CompareTag("Enemy")) otherGO.GetComponent<BaseEnemy>().TakeDamageFromSnowball(gameObject);
            Destroy(gameObject);
        }
    }
}
