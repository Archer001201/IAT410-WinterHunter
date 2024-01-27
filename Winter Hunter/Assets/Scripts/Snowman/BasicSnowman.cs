using Player;
using UnityEngine;

namespace Snowman
{
    public class BasicSnowman : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public float maxHealth;
        public float summoningTimer;
        public float summoningCost;
        [Header("Dynamic Attributes")] 
        public float health;
        

        private PlayerController _playerController;
        private float _startTime;

        private void Awake()
        {
            health = maxHealth;
            _startTime = Time.time;
        }

        private void Start()
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _playerController.canSummonSnowman = false;
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            
            if (health <= 0 || Time.time - _startTime >= summoningTimer)
            {
                _playerController.canSummonSnowman = true;
                Destroy(gameObject);
            }
        }
    }
}
