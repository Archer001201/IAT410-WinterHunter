using System;
using Enemy;
using Player;
using UnityEngine;

namespace Snowball
{
    /*
     * Rolling snowball
     */
    public class RollingSnowball : BaseSnowball
    {
        public float rollingDistance;
        public Vector2 rollingSize;
        private Vector3 _lastPosition;
        private float _accumulatedDistance;
        private PlayerController _playerController;
        private bool _isReleasing;

        protected override void Awake()
        {
            base.Awake();
            _accumulatedDistance = 0f;
            transform.localScale = new Vector3(rollingSize.x, rollingSize.x, rollingSize.x);
            _playerController = PlayerAttr.gameObject.GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (transform.localScale.x > rollingSize.y && !_isReleasing)
            {
                _playerController.OnRollingSnowballEnd();
            }
        }
        
        private void FixedUpdate()
        {
            if (!_isReleasing) return;
            var distanceMoved = Vector3.Distance(transform.position, _lastPosition);
            _accumulatedDistance += distanceMoved;
            
            _lastPosition = transform.position;
            
            if (_accumulatedDistance > rollingDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            if (otherGO.CompareTag("Enemy"))
            {
                otherGO.GetComponent<BaseEnemy>().TakeDamage(damage, shieldBreakEfficiency);
                PlayerAttr.mana += damage * PlayerAttr.manaRecovery;
            }
            if (!otherGO.CompareTag("Ground") && !otherGO.CompareTag("Projectile")) Destroy(gameObject);
        }

        /*
         * Force release snowball when rolling snowball reached the max size
         */
        public void SetReleasingState()
        {
            _isReleasing = true;
            _lastPosition = transform.position;
        }
    }
}
