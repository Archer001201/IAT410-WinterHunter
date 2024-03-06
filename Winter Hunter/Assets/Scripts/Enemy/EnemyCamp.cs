using System.Collections.Generic;
using Props;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Enemy
{
    /*
     * Store data of enemies and chests in the camp
     */
    public class EnemyCamp : MonoBehaviour
    {
        public UnityEvent onCampCleared;
        public List<BaseEnemy> enemyList;
        public List<TreasureChest> chestList;
        public int waveThreshold;
        public int enemiesPerWave;
        public float raycastDistance;
        public bool isCleared;

        private GameObject _player;
        private readonly List<GameObject> _enemiesOnStandby = new();

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");

            foreach (var enemy in enemyList)
            {
                if (!enemy.gameObject.activeSelf) _enemiesOnStandby.Add(enemy.gameObject);
            }
        }

        private void Update()
        {
            if (enemyList.Count < 1 && !isCleared)
            {
                isCleared = true;
                onCampCleared?.Invoke();
            }
            
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null) enemyList.Remove(enemyList[i]);
            }
            
            foreach (var t in chestList)
            {
                t.canOpen = isCleared;
            }
            
            CheckPlayerInRaycastRange();
            UpdateEnemyWave();
        }

        private void CheckPlayerInRaycastRange()
        {
            var dir = _player.transform.position - transform.position;

            var layerMask =  1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Player");

            NotifyEnemiesToChangeChasingState(
                Physics.Raycast(transform.position, dir.normalized, out var hit, raycastDistance, layerMask) &&
                hit.collider.CompareTag("Player"));
            
            Debug.DrawLine(transform.position, _player.transform.position, Color.blue);
        }

        private void NotifyEnemiesToChangeChasingState(bool isPlayerInCampRange)
        {
            foreach (var enemy in enemyList)
            {
                if (enemy.isPlayerInCampRange == isPlayerInCampRange) return;
                enemy.isPlayerInCampRange = isPlayerInCampRange;
                enemy.SetChaseTarget();
                // if (isChasing && enemy.CurrentState != enemy.ChaseState)
                //     enemy.SwitchState(EnemyState.Chase);
            }
        }

        private void UpdateEnemyWave()
        {
            var activatedAmount = enemyList.Count - _enemiesOnStandby.Count;
            if (activatedAmount > waveThreshold) return;
            for (var i = 0; i < _enemiesOnStandby.Count; i++)
            {
                if (i >= enemiesPerWave) return;
                var enemy = _enemiesOnStandby[i];
                enemy.SetActive(true);
                _enemiesOnStandby.RemoveAt(i);
            }
        }
    }
}
