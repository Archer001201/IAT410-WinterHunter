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

        public float maxHealth;
        public float maxStamina;
        public float maxMana;
        public float speed;
        public float staminaRecovery;
        
        public float health;
        public float stamina;
        public float mana;
        public float attack;
        public float manaRecovery;

        public List<SnowmanInfo> snowmanList;

        public bool isInvincible;

        public List<GameObject> enemiesInCombat;
        public bool isInCombat;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            maxHealth = _playerSO.maxHealth;
            maxStamina = _playerSO.maxStamina;
            maxMana = _playerSO.maxMana;
            
            health = _playerSO.currentHealth;
            stamina = _playerSO.currentStamina;
            mana = _playerSO.currentMana;
            // mana = 0;
            attack = _playerSO.attack;
            manaRecovery = _playerSO.manaRecovery;
            staminaRecovery = _playerSO.staminaRecovery;
            speed = _playerSO.speed;
            
            LoadSnowmanList();
            
            // _playerSO.SaveData();
            if (_playerSO.levelSo.respawnAtThisPosition)
            {
                transform.position = _playerSO.levelSo.position;
                _playerSO.levelSo.respawnAtThisPosition = false;
            }
            
            _playerSO.SaveData();
        }

        private void OnEnable()
        {
            EventHandler.OnOpenSnowmanChest += AddSnowmanToPlayer;
            EventHandler.OnAddEnemyToCombatList += enemy => enemiesInCombat.Add(enemy);
            EventHandler.OnRemoveEnemyToCombatList += enemy => enemiesInCombat.Remove(enemy);
        }
        
        private void OnDisable()
        {
            EventHandler.OnOpenSnowmanChest -= AddSnowmanToPlayer;
        }

        private void Update()
        {
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            health = Mathf.Clamp(health, 0, maxHealth);
            mana = Mathf.Clamp(mana, 0, maxMana);
            
            if (health <= 0) EventHandler.PlayerDie();

            isInCombat = enemiesInCombat.Count > 0;
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
            snowmanList.Clear();
            for (var i = 0; i < _playerSO.snowmanList.Count; i++)
            {
                var snowmanTypeAndLevel = _playerSO.snowmanList[i];
                var snowmanSO = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + snowmanTypeAndLevel.type + "_SO");
                
                snowmanList.Add(new SnowmanInfo());
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
            _playerSO.SaveData();
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
