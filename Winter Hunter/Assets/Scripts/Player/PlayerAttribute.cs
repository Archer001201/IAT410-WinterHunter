using System;
using System.Collections.Generic;
using DataSO;
using Snowman;
using UnityEngine;

namespace Player
{
    public class PlayerAttribute : MonoBehaviour
    {
        private PlayerSO _playerSO;

        public float health;
        public float stamina;
        public float energy;

        public List<SnowmanInfor> snowmanList;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            health = _playerSO.maxHealth;
            stamina = _playerSO.maxStamina;
            energy = 0;

            // Debug.Log(_playerSO.snowmanList.Count);
            for (var i = 0; i < _playerSO.snowmanList.Count; i++)
            {
                var snowmanType = _playerSO.snowmanList[i];
                SnowmanSO snowmanSO = null;
                switch (snowmanType)
                {
                    case SnowmanType.MeatShield:
                        snowmanSO = Resources.Load<SnowmanSO>("DataSO/MeatShield_SO");
                        break;
                    case SnowmanType.Healer:
                        snowmanSO = Resources.Load<SnowmanSO>("DataSO/Healer_SO");
                        break;
                    case SnowmanType.Normal:
                        break;
                    case SnowmanType.MothToTheFlame:
                        break;
                    case SnowmanType.Bomb:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                // Debug.Log(i);
                if (snowmanList.Count <= i) snowmanList.Add(new SnowmanInfor());
                snowmanList[i].type = snowmanType;
                if (snowmanSO == null) continue;
                snowmanList[i].cooldown = snowmanSO.cooldown;
                snowmanList[i].cooldownTimer = 0;
                snowmanList[i].canBeSummoned = true;
                snowmanList[i].summoningCost = snowmanSO.summoningCost;
            }
        }

        private void Update()
        {
            stamina = Mathf.Clamp(stamina, 0, _playerSO.maxStamina);
            health = Mathf.Clamp(health, 0, _playerSO.maxHealth);
            energy = Mathf.Clamp(energy, 0, _playerSO.maxEnergy);
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
    }
}
