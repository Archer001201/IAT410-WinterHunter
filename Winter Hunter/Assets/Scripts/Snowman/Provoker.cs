using System.Collections.Generic;
using BTFrame;
using Enemy;
using EventSystem;
using Snowman.Skills;
using UnityEngine;

namespace Snowman
{
    /*
     * A type of snowman can taunt enemy
     */
    public class Provoker : BaseSnowman
    {
        public float tauntRange;
        public float chaseRange;
        public float attackBonusFactor;
        public GameObject explosionPrefab;
        
        private SphereCollider _sphereCollider;
        private readonly List<BaseEnemy> _detectedEnemyScripts = new();

        protected override void Awake()
        {
            base.Awake();
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = chaseRange;
        }

        protected override void Update()
        {
            base.Update();
            SetTargetForEnemies();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.CompareTag("Enemy")) _detectedEnemyScripts.Add(other.gameObject.GetComponent<BaseEnemy>());
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if (other.CompareTag("Enemy")) _detectedEnemyScripts.Remove(other.gameObject.GetComponent<BaseEnemy>());
        }

        private void SetTargetForEnemies()
        {
            for (var i = 0; i < _detectedEnemyScripts.Count; i++)
            {
                var enemy = _detectedEnemyScripts[i];
                if (enemy == null)
                {
                    _detectedEnemyScripts.RemoveAt(i);
                    continue;
                }
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= tauntRange) enemy.SetTarget(gameObject);
            }
        }

        protected override void DestroyMe()
        {
            var explosionGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionGO.GetComponent<SnowmanExplosion>().SetAttack(MySnowmanSO.attack * (1 + attackBonusFactor * (_detectedEnemyScripts.Count-1)), MySnowmanSO.shieldBreakEfficiency);
            
            base.DestroyMe();
        }
    }
}
