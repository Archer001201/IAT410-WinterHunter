using System;
using Enemy;
using Player;
using UnityEngine;

namespace Snowman.Skills
{
    public class SnowmanSlash : MonoBehaviour
    {
        public float attackDuration = 1f;
        public ParticleSystem slashTrails;
        private float _rotationSpeed;
        private float _currentTime;
        private float _attack;
        private Collider _collider;
        private bool _isAdvanced;
        private PlayerAttribute _playerAttr;

        private void Awake()
        {
            _rotationSpeed = 360.0f / attackDuration;
            _collider = GetComponent<BoxCollider>();
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            var rotationThisFrame = _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationThisFrame, 0);
            if (_currentTime > attackDuration)
            {
                _collider.enabled = false;
            }
            if (slashTrails.isStopped)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(_attack);

            if (_isAdvanced)
            {
                foreach (var snowman in _playerAttr.snowmanList)
                {
                    snowman.cooldownTimer -= 1;
                }
            }
        }

        public void SetAttack(float attack, bool isAdvanced)
        {
            _attack = attack;
            _isAdvanced = isAdvanced;
        }
    }
}
