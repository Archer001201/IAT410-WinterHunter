using System.Collections.Generic;
using Props;
using UnityEngine;

namespace Enemy
{
    /*
     * Store data of enemies and chests in the camp
     */
    public class EnemyCamp : MonoBehaviour
    {
        public List<BaseEnemy> enemyList;
        public List<Chest> chestList;
        public bool isCleared;
        public float raycastDistance;

        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (enemyList.Count < 1 && !isCleared) isCleared = true;
            
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null) enemyList.Remove(enemyList[i]);
            }
            
            foreach (var t in chestList)
            {
                t.canOpen = isCleared;
            }
            
            CheckPlayerInRaycastRange();
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

        private void NotifyEnemiesToChangeChasingState(bool isChasing)
        {
            foreach (var enemy in enemyList)
            {
                enemy.isChasing = isChasing;
            }
        }
    }
}
