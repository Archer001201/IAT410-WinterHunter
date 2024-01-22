using System;
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
            // if (Vector3.Distance(transform.position,_startPosition) >= rollingDistance) Destroy(gameObject);
            if (transform.localScale.x > rollingSize.y && !_isReleasing)
            {
                _playerController.ReleaseSnowball();
            }
        }
        
        private void FixedUpdate()
        {
            if (!_isReleasing) return;
            // 计算自上一帧以来物体移动的距离
            var distanceMoved = Vector3.Distance(transform.position, _lastPosition);
            _accumulatedDistance += distanceMoved;

            // 更新 _lastPosition 为当前帧的位置
            _lastPosition = transform.position;

            // 如果累积的移动距离超过了 rollingDistance，则销毁物体
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
