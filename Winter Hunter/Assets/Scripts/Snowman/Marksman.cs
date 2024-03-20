using System;
using System.Collections;
using Snowman.Skills;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Snowman
{
    public class Marksman : BaseSnowman
    {
        public GameObject projectile;
        public Transform firePoint;
        private Transform _target;
        private Rigidbody _rb;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(Fire());
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_target == null) return;
            var direction = (_target.transform.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            
            _rb.MoveRotation(lookRotation);
        }

        private IEnumerator Fire()
        {
            while (health > 0)
            {
                if (detectedEnemies.Count > 0)
                {
                    var randNum = Random.Range(0, detectedEnemies.Count-1);
                    _target = detectedEnemies[randNum];
                }
                
                var projectileGo = Instantiate(projectile, firePoint.position, Quaternion.identity);
                projectileGo.GetComponent<IceProjectile>().SetProjectile(level == SnowmanLevel.Advanced,transform.forward, MySnowmanSO.attack, MySnowmanSO.shieldBreakEfficiency);
                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }
    }
}
