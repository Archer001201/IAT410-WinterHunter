using UnityEngine;

namespace Enemy
{
    public class DummyEnemy : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Projectile")) Destroy(gameObject);
        }
    }
}
