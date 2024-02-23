using BTFrame;
using DataSO;
using UnityEngine;
using UnityEngine.AI;
using EventHandler = EventSystem.EventHandler;

namespace Snowman
{
    /*
     * Super class of snowman
     */
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
        private float _startTime;
        

        protected virtual void Awake()
        {
            PlayerGO = GameObject.FindWithTag("Player");
            summoningCost = snowmanSO.manaCost;

            health = snowmanSO.health;
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
            health = Mathf.Clamp(health, 0, snowmanSO.health);
            summoningTimer = Time.time - _startTime;

            if (health <= 0 || summoningTimer >= snowmanSO.manaCost)
            {
                Destroy(gameObject);
            }
            
            BTree?.BTUpdate();
        }
        
        /*
         * Set up and initialize behaviour tree
         */
        protected virtual void SetUpBehaviorTree(){}
        
        /*
         * Set target's position as destination for NavMesh Agent
         */
        protected void SetNavigation()
        {
            if (_agent.isActiveAndEnabled && TargetTrans != null)
            {
                _agent.SetDestination(TargetTrans.position);
            }
        }
        
        /*
         * Set NavMesh Agent continue to move
         */
        protected void StartChase()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (_agent.isStopped) _agent.isStopped = false;
        }

        /*
         * Set NavMesh Agent stop moving
         */
        protected void StopChase()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (!_agent.isStopped) _agent.isStopped = true;
        }

        /*
         * Destroy this game object
         */
        protected virtual void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
