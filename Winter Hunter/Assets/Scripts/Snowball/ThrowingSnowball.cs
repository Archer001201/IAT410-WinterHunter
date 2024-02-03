using UnityEngine;

namespace Snowball
{
    public class ThrowingSnowball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);
        }
    }
}
