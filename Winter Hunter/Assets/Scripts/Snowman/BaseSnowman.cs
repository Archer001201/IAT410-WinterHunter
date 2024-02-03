using System;
using BTFrame;
using DataSO;
using Player;
using UnityEngine;
using UnityEngine.AI;
using EventHandler = EventSystem.EventHandler;

namespace Snowman
{
    public class BaseSnowman : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public float healthFactor;
        public float attackFactor;
        public float summoningTime;
        public float summoningCost;
        public float followRange;
        public static float Cooldown;
        [Header("Dynamic Attributes")] 
        public float maxHealth;
        public float health;
        public float summoningTimer;
        public float attack;
        public static float CooldownTimer;
        [Header("Component Settings")] 
        public GameObject hudCanvas;
        
        // ReSharper disable once MemberCanBePrivate.Global
        protected PlayerSO PlayerSO;
        protected BehaviorTree BTree;
        protected GameObject PlayerGO;
        protected Transform TargetTrans;
        
        private NavMeshAgent _agent;
        private PlayerController _playerController;
        private float _startTime;
        

        protected virtual void Awake()
        {
            PlayerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            PlayerGO = GameObject.FindWithTag("Player");
            _playerController = PlayerGO.GetComponent<PlayerController>();
            // _playerController.canSummonSnowman = false;
            
            maxHealth = healthFactor * PlayerSO.maxHealth;
            health = maxHealth;
            attack = attackFactor * PlayerSO.attack;
            _startTime = Time.time;
            
            hudCanvas.SetActive(true);
            _agent = GetComponent<NavMeshAgent>();
            
            SetUpBehaviorTree();
        }

        private void OnEnable()
        {
            EventHandler.OnDestroyExistedSnowman += DestroyMe;
        }

        private void OnDisable()
        {
            EventHandler.OnDestroyExistedSnowman -= DestroyMe;
        }

        protected virtual void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            summoningTimer = Time.time - _startTime;

            if (health <= 0 || summoningTimer >= summoningTime)
            {
                // _playerController.canSummonSnowman = true;
                Destroy(gameObject);
            }
            
            BTree?.BTUpdate();
        }
        
        protected virtual void SetUpBehaviorTree(){}
        
        protected void SetNavigation()
        {
            if (_agent.isActiveAndEnabled && TargetTrans != null)
            {
                _agent.SetDestination(TargetTrans.position);
            }
        }
        
        protected void StartChase()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (_agent.isStopped) _agent.isStopped = false;
        }

        protected void StopChase()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (!_agent.isStopped) _agent.isStopped = true;
        }

        protected virtual void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
