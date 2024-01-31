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
        public float followRange;
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
            health = maxHealth;
            _startTime = Time.time;
            
            hudCanvas.SetActive(true);
            
            _agent = GetComponent<NavMeshAgent>();
            
            PlayerGO = GameObject.FindWithTag("Player");
            _playerController = PlayerGO.GetComponent<PlayerController>();
            _playerController.canSummonSnowman = false;
            
            SetUpBehaviorTree();
        }

        protected virtual void Update()
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
    }
}
