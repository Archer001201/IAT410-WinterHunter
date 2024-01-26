using UnityEngine;

namespace Enemy
{
    public class DummyEnemy : BasicEnemy
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Projectile")) Destroy(gameObject);
        }
    }
}
