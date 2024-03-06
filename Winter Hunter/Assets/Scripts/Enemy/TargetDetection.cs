using System;
using UnityEngine;

namespace Enemy
{
    public class TargetDetection : MonoBehaviour
    {
        public BaseEnemy enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemy.detectedPlayer = other.transform;
                enemy.SetChaseTarget();
            }

            if (other.CompareTag("Snowman"))
            {
                enemy.detectedSnowman = other.transform;
                enemy.SetChaseTarget();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemy.detectedPlayer = null;
                enemy.SetChaseTarget();
            }

            if (other.CompareTag("Snowman"))
            {
                enemy.detectedSnowman = null;
                enemy.SetChaseTarget();
            }
        }
    }
}
