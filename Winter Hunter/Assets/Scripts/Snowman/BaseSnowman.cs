using BTFrame;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Snowman
{
    public class BaseSnowman : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public float maxHealth;
        public float summoningTime;
        public float summoningCost;
        [Header("Dynamic Attributes")] 
        public float health;
        public float summoningTimer;
        [Header("Component Settings")] 
        public GameObject hudCanvas;
        
        protected BehaviorTree BTree;
        protected NavMeshAgent Agent;
        protected GameObject PlayerGO;
        
        private PlayerController _playerController;
        private float _startTime;

        private void Awake()
        {
            health = maxHealth;
            _startTime = Time.time;
            
            hudCanvas.SetActive(true);
            
            Agent = GetComponent<NavMeshAgent>();
            
            PlayerGO = GameObject.FindWithTag("Player");
            _playerController = PlayerGO.GetComponent<PlayerController>();
            _playerController.canSummonSnowman = false;
            
            SetUpBehaviorTree();
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            summoningTimer = Time.time - _startTime;
            if (health <= 0 || summoningTimer >= summoningTime)
            {
                _playerController.canSummonSnowman = true;
                Destroy(gameObject);
            }
            
            BTree?.BTUpdate();
        }
        
        protected virtual void SetUpBehaviorTree(){}
    }
}
