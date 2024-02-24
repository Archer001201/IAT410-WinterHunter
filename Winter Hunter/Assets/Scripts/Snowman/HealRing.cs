using System.Collections;
using Player;
using UnityEngine;
using Utilities;

namespace Snowman
{
    /*
     * The skill of snowman that can heal player by staying at the healing range
     */
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
            
            if (healerAttr.level == SnowmanLevel.Advanced) Debug.Log("advanced");
        }

        private void OnTriggerExit(Collider other)
        {
            if (_healCoroutine == null) return;
            StopCoroutine(_healCoroutine);
            _healCoroutine = null;
            
            if (healerAttr.level == SnowmanLevel.Advanced) Debug.Log("clear effect");
        }

        /*
         * Every a specific time heal player based on healer's attack
         */
        private IEnumerator Heal()
        {
            while (_playerAttr != null)
            {
                _playerAttr.health += healerAttr.snowmanSO.attack;
                yield return new WaitForSeconds(healerAttr.snowmanSO.attackSpeed);
            }
        }
    }
}
