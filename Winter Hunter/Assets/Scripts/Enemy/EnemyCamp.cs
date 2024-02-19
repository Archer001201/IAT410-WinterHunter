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
        public List<GameObject> enemyList;
        public List<Chest> chestList;
        public bool isCleared;

        private void Awake()
        {
            foreach (var enemy in enemyList)
            {
                var baseEnemy = enemy.GetComponent<BaseEnemy>();
                baseEnemy.campTrans = transform;
            }
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            foreach (var enemy in enemyList)
            {
                enemy.GetComponent<BaseEnemy>().isChasing = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            foreach (var enemy in enemyList)
            {
                enemy.GetComponent<BaseEnemy>().isChasing = false;
            }
        }
    }
}
