using System;
using Player;
using Snowman;
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
                enemy.detectedPlayer = other.GetComponent<PlayerAttribute>();
                enemy.SetChaseTarget();
            }

            if (other.CompareTag("Snowman"))
            {
                enemy.detectedSnowman = other.GetComponent<BaseSnowman>();
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