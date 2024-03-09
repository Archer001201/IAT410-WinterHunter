using System.Collections.Generic;
using Player;
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
        public float maxMana;
        public float attack;
        public float speed;
        public float staminaRecovery;
        public float manaRecovery;

        [Header("Dynamic Attributes")] 
        public float currentHealth;
        public float currentStamina;
        public float currentMana;
        public List<SnowmanTypeAndLevel> snowmanList;

        public void SaveData()
        {
            var playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
            currentHealth = playerAttr.health;
            currentMana = playerAttr.mana;
            currentStamina = playerAttr.stamina;
        }
    }
}
