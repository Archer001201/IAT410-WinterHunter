using System;
using System.Collections;
using UnityEngine;

namespace Snowman.Skills
{
    public class FrozenRing : SnowmanExplosion
    {
        public float minRadius;
        public float maxRadius;
        public float duration;
        
        private SphereCollider _sphereCollider;
        private Transform _player;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = 1;
            _player = GameObject.FindWithTag("Player").transform;
            StartCoroutine(GrowRadiusOverTime(minRadius, maxRadius, duration));
        }
        
        private IEnumerator GrowRadiusOverTime(float startRadius, float endRadius, float timer)
        {
            float currentTime = 0;
            _sphereCollider.radius = startRadius;
            while (currentTime < timer)
            {
                currentTime += Time.deltaTime;
                _sphereCollider.radius = Mathf.Lerp(startRadius, endRadius, currentTime / timer);
                yield return null; // 等待下一帧
            }
            // _sphereCollider.radius = endRadius; // 确保最终值为6
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_player == null) return;
            transform.position = _player.position;
        }
    }
}
