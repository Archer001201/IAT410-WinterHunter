using System.Collections.Generic;
using Snowball;
using Snowman;
using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(menuName = "Create Player_SO", fileName = "Player_SO", order = 0)]
    public class PlayerSO : UnityEngine.ScriptableObject
    {
        [Header("Static Attributes")]
        public float maxHealth;
        public float maxStamina;
        public float maxEnergy;
        public float attack;
        public float speed;
        public float staminaRecovery;
        public List<SnowmanType> snowmanList;

        [Header("Dynamic Attributes")] 
        public float currentHealth;
        public float currentStamina;
        public float currentEnergy;
    }
}
