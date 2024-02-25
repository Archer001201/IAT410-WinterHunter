using System;
using System.Collections;
using BTFrame;
using DataSO;
using Player;
using Snowman.Skills;
using UnityEngine;
using UnityEngine.VFX;
using Utilities;

namespace Snowman
{
    /*
     * A type of snowman that can heal player by the healing ring
     */
    public class Healer : BaseSnowman
    {
        public VisualEffect visualEffect;
        public float vfxLengthDivisor;
        public float healRange;
        public float overflowHealing;
        public float frozenRingThreshold;
        public GameObject frozenRingPrefab;
        public float attackFactor;
        
        private Transform _playerTransform;
        private Coroutine _healCoroutine;
        private PlayerAttribute _playerAttr;
        private PlayerSO _playerSO;
        private SphereCollider _healRangeCollider;

        protected override void Awake()
        {
            base.Awake();
            overflowHealing = 0;
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            _playerAttr = _playerTransform.gameObject.GetComponent<PlayerAttribute>();
            _healRangeCollider = GetComponent<SphereCollider>();
            _healRangeCollider.radius = healRange;
        }

        protected override void Update()
        {
            base.Update();
            visualEffect.transform.LookAt(_playerTransform);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (!other.CompareTag("Player")) return;
            visualEffect.Play();
            _healCoroutine ??= StartCoroutine(Heal());
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            visualEffect.SetFloat("Length", distance/vfxLengthDivisor);

            if (level == SnowmanLevel.Advanced)
            {
                if (overflowHealing < frozenRingThreshold) return;
                var frozenRingGO = Instantiate(frozenRingPrefab, transform.position, Quaternion.identity);
                frozenRingGO.GetComponent<SnowmanExplosion>().SetAttack(MySnowmanSO.attack*attackFactor, MySnowmanSO.shieldBreakEfficiency);
                overflowHealing = 0;
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if (!other.CompareTag("Player")) return;
            visualEffect.Stop();
            if (_healCoroutine == null) return;
            StopCoroutine(_healCoroutine);
            _healCoroutine = null;
            overflowHealing = 0;
        }
        
        private IEnumerator Heal()
        {
            while (_playerAttr != null)
            {
                if (_playerAttr.health < _playerSO.maxHealth) _playerAttr.ReceiveHealing(MySnowmanSO.attack);
                else overflowHealing += MySnowmanSO.attack;
                yield return new WaitForSeconds(MySnowmanSO.attackSpeed);
            }
        }
    }
}
