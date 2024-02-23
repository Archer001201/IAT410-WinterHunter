using System;
using System.Collections.Generic;
using DataSO;
using Snowman;
using UnityEngine;
using Utilities;
using EventHandler = EventSystem.EventHandler;

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

        public List<SnowmanInfo> snowmanList;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            health = _playerSO.maxHealth;
            stamina = _playerSO.maxStamina;
            mana = 0;
            
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
                        snowman.cooldownTimer = 0;
                    }
                }
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
                // SnowmanSO snowmanSO = null;
                var snowmanSO = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + snowmanTypeAndLevel.type + "_SO");
                // switch (snowmanType)
                // {
                //         snowmanSO = Resources.Load<SnowmanSO>("DataSO/");
                //     case Enums.SnowmanType.Warrior:
                //         snowmanSO = Resources.Load<SnowmanSO>("DataSO/");
                //         break;
                //     case Enums.SnowmanType.Healer:
                //         snowmanSO = Resources.Load<SnowmanSO>("DataSO/Healer_SO");
                //         break;
                //     case Enums.SnowmanType.Guardian:
                //         break;
                //     case Enums.SnowmanType.Marksman:
                //         break;
                //     case Enums.SnowmanType.Provoker:
                //         break;
                //     case Enums.SnowmanType.Alchemist:
                //         break;
                //     default:
                //         throw new ArgumentOutOfRangeException();
                // }
                
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
        private void AddSnowmanToPlayer(List<SnowmanTypeAndLevel> snowmanTypes)
        {
            foreach (var item in snowmanTypes)
            {
                // if (!_playerSO.snowmanList.Contains(item))
                // {
                //     _playerSO.snowmanList.Add(item);
                // }
                // else
                // {
                //     var foundItem = _playerSO.snowmanList.Find(x => x.type == item.type);
                //     if (foundItem != null)
                //     {
                //         foundItem.level = Enums.SnowmanLevel.Advanced;
                //     }
                // }
                var foundItem = _playerSO.snowmanList.Find(x => x.type == item.type);
                if (foundItem != null)
                {
                    foundItem.level = Enums.SnowmanLevel.Advanced;
                }
                else
                {
                    _playerSO.snowmanList.Add(item);
                }
            }
            LoadSnowmanList();
            EventHandler.UpdateSkillPanel();
        }
    }
}
