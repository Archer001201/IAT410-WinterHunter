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
            if (other.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);
        }
    }
}
