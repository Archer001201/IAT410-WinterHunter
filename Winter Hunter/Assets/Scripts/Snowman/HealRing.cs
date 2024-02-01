using System.Collections;
using Player;
using UnityEngine;

namespace Snowman
{
    public class HealRing : MonoBehaviour
    {
        public Healer healerAttr;
        private Coroutine _healCoroutine;
        private PlayerAttribute _playerAttr;

        private void Awake()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _healCoroutine ??= StartCoroutine(Heal());
        }

        private void OnTriggerExit(Collider other)
        {
            if (_healCoroutine == null) return;
            StopCoroutine(_healCoroutine);
            _healCoroutine = null;
        }

        private IEnumerator Heal()
        {
            while (_playerAttr != null)
            {
                _playerAttr.health += healerAttr.attack;
                yield return new WaitForSeconds(healerAttr.healTimer);
            }
        }
    }
}
