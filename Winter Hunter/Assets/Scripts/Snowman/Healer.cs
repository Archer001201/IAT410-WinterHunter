using System;
using System.Collections;
using BTFrame;
using Player;
using UnityEngine;
using UnityEngine.VFX;

namespace Snowman
{
    /*
     * A type of snowman that can heal player by the healing ring
     */
    public class Healer : BaseSnowman
    {
        public VisualEffect visualEffect;
        public Transform playerTransform;
        private Coroutine _healCoroutine;
        private PlayerAttribute _playerAttr;
        

        protected override void Awake()
        {
            base.Awake();
            playerTransform = GameObject.FindWithTag("Player").transform;
            _playerAttr = playerTransform.gameObject.GetComponent<PlayerAttribute>();
        }

        protected override void Update()
        {
            base.Update();
            visualEffect.transform.LookAt(playerTransform);
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
            var distance = Vector3.Distance(transform.position, playerTransform.position);
            visualEffect.SetFloat("Length", distance/6f);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if (!other.CompareTag("Player")) return;
            visualEffect.Stop();
            if (_healCoroutine == null) return;
            StopCoroutine(_healCoroutine);
            _healCoroutine = null;
        }
        
        private IEnumerator Heal()
        {
            while (_playerAttr != null)
            {
                _playerAttr.health += snowmanSO.attack;
                yield return new WaitForSeconds(snowmanSO.attackSpeed);
            }
        }
    }
}
