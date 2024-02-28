using System;
using System.Collections.Generic;
using DataSO;
using Snowman;
using UnityEngine;
using Utilities;
using EventHandler = Utilities.EventHandler;

namespace Player
{
    /*
     * Store and handle player's attributes
     */
    public class PlayerAttribute : MonoBehaviour
    {
        private PlayerSO _playerSO;

        public float health;
        public float stamina;
        public float mana;
        public float attack;
        public float manaRecovery;

        public List<SnowmanInfo> snowmanList;

        public bool isInvincible;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            health = _playerSO.maxHealth;
            stamina = _playerSO.maxStamina;
            mana = 0;
            attack = _playerSO.attack;
            manaRecovery = _playerSO.manaRecovery;
            
            LoadSnowmanList();
        }

        private void OnEnable()
        {
            EventHandler.OnOpenSnowmanChest += AddSnowmanToPlayer;
        }
        
        private void OnDisable()
        {
            EventHandler.OnOpenSnowmanChest -= AddSnowmanToPlayer;
        }

        private void Update()
        {
            stamina = Mathf.Clamp(stamina, 0, _playerSO.maxStamina);
            health = Mathf.Clamp(health, 0, _playerSO.maxHealth);
            mana = Mathf.Clamp(mana, 0, _playerSO.maxMana);
            
            if (health <= 0) EventHandler.PlayerDie();
        }

        private void FixedUpdate()
        {
            foreach (var snowman in snowmanList)
            {
                if (!snowman.canBeSummoned)
                {
                    if (snowman.cooldownTimer > 0) snowman.cooldownTimer -= Time.fixedDeltaTime;
                    else
                    {
                        snowman.canBeSummoned = true;
                    }
                }

                snowman.cooldownTimer = Mathf.Clamp(snowman.cooldownTimer, 0, snowman.cooldown);
            }
        }

        /*
         * Load and fresh snowman list from player scriptable object
         */
        private void LoadSnowmanList()
        {
            for (var i = 0; i < _playerSO.snowmanList.Count; i++)
            {
                var snowmanTypeAndLevel = _playerSO.snowmanList[i];
                var snowmanSO = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + snowmanTypeAndLevel.type + "_SO");
                
                if (snowmanList.Count <= i) snowmanList.Add(new SnowmanInfo());
                snowmanList[i].type = snowmanTypeAndLevel.type;
                snowmanList[i].level = snowmanTypeAndLevel.level;
                if (snowmanSO == null) continue;
                snowmanList[i].cooldown = snowmanSO.cooldown;
                snowmanList[i].cooldownTimer = 0;
                snowmanList[i].canBeSummoned = true;
                snowmanList[i].summoningCost = snowmanSO.manaCost;
            }
        }

        /*
         * when player opened a snowman chest, add the snowman into snowman list in player scriptable object, and notice skill panel to update icons
         */
        private void AddSnowmanToPlayer(SnowmanTypeAndLevel snowman)
        {
            // foreach (var item in snowmanTypes)
            // {
            var foundItem = _playerSO.snowmanList.Find(x => x.type == snowman.type);
            if (foundItem != null) 
            { 
                foundItem.level = SnowmanLevel.Advanced;
            }
            else 
            { 
                _playerSO.snowmanList.Add(snowman);
            }
            // }
            LoadSnowmanList();
            EventHandler.UpdateSkillPanel();
        }

        public void TakeDamage(float damage)
        {
            if (isInvincible) return;
            health -= damage;
        }

        public void ReceiveHealing(float healing)
        {
            health += healing;
        }
    }
}
