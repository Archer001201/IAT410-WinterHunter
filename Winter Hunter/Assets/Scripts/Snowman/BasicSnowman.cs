using System;
using Player;
using UnityEngine;

namespace Snowman
{
    public class BasicSnowman : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public float maxHealth;
        public float maxSummoningTimer;
        public float castingCost;
        public SnowmanType type;
        [Header("Dynamic Attributes")] 
        public float health;
        public float summoningTimer;

        private PlayerController _playerController;

        private void Awake()
        {
            Debug.Log(type);
        }

        private void Start()
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _playerController.canSummonSnowman = false;
        }

        private void Update()
        {
            if (health <= 0 || summoningTimer <= 0)
            {
                _playerController.canSummonSnowman = true;
                Destroy(gameObject);
            }
        }
    }
}
