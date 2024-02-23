using System.Collections.Generic;
using Snowman;
using UnityEngine;
using Utilities;

namespace DataSO
{
    /*
     * Store player's attribute
     */
    [CreateAssetMenu(menuName = "ScriptableObject/Create Player_SO", fileName = "Player_SO")]
    public class PlayerSO : ScriptableObject
    {
        [Header("Static Attributes")]
        public float maxHealth;
        public float maxStamina;
        public float maxEnergy;
        public float attack;
        public float speed;
        public float staminaRecovery;

        [Header("Dynamic Attributes")] 
        public float currentHealth;
        public float currentStamina;
        public float currentEnergy;
        public List<Enums.SnowmanType> snowmanList;
    }
}
