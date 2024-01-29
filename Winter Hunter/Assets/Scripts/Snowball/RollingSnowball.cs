using Player;
using UnityEngine;

namespace Snowball
{
    public class RollingSnowball : MonoBehaviour
    {
        public float rollingDistance;
        public Vector2 rollingSize;
        private Vector3 _lastPosition;
        private float _accumulatedDistance;
        private PlayerController _playerController;
        private bool _isReleasing;

        private void Awake()
        {
            _accumulatedDistance = 0f;
            transform.localScale = new Vector3(rollingSize.x, rollingSize.x, rollingSize.x);
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
            if (other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
        }

        public void SetReleasingState()
        {
            _isReleasing = true;
            _lastPosition = transform.position;
        }
    }
}
