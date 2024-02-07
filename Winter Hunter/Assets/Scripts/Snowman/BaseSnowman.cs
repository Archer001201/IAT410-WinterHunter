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
        public SnowmanSO snowmanSO;
        public float followRange;
        public float summoningCost;
        [Header("Dynamic Attributes")] 
        public float health;
        public float summoningTimer;
        [Header("Component Settings")] 
        public GameObject hudCanvas;
        
        protected BehaviorTree BTree;
        protected GameObject PlayerGO;
        protected Transform TargetTrans;
        
        private NavMeshAgent _agent;
        private PlayerController _playerController;
        private float _startTime;
        

        protected virtual void Awake()
        {
            PlayerGO = GameObject.FindWithTag("Player");
            _playerController = PlayerGO.GetComponent<PlayerController>();
            summoningCost = snowmanSO.summoningCost;

            health = snowmanSO.maxHealth;
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
            health = Mathf.Clamp(health, 0, snowmanSO.maxHealth);
            summoningTimer = Time.time - _startTime;

            if (health <= 0 || summoningTimer >= snowmanSO.summoningTime)
            {
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
