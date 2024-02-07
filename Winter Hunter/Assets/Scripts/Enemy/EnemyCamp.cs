using System;
using System.Collections.Generic;
using System.Reflection;
using Props;
using UnityEngine;

namespace Enemy
{
    public class EnemyCamp : MonoBehaviour
    {
        public List<GameObject> enemyList;
        public List<Chest> chestList;
        public float campRange;
        public bool isCleared;

        private void Awake()
        {
            foreach (var enemy in enemyList)
            {
                var baseEnemy = enemy.GetComponent<BaseEnemy>();
                baseEnemy.campTrans = transform;
                baseEnemy.campRange = campRange;
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
    }
}
