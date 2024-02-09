using System.Collections;
using Player;
using Snowball;
using UnityEngine;
using BTFrame;
using EventSystem;
using UnityEngine.AI;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        [Header("Static Attributes")]
        public float maxHealth;
        public float maxShield;
        public float resistance;
        public float chaseRange;
        public float attackRange;
        public float attackDamage;
        public float campRange;
        public Transform campTrans;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        [Header("Component Settings")]
        public GameObject hudCanvas;
        public GameObject target;

        protected Transform TargetTrans;
        protected BehaviorTree BTree;
        private NavMeshAgent _agent;
        
        private GameObject _player;
        private PlayerAttribute _playerAttr;
        private Coroutine _attackCoroutine;
        private Coroutine _patrolCoroutine;
        private Vector3 _originalPosition;
        

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            _agent = GetComponent<NavMeshAgent>();
            
            _player = GameObject.FindWithTag("Player");
            _playerAttr = _player.GetComponent<PlayerAttribute>();
            
            UpdateTarget(_player);
            
            SetUpBehaviorTree();

            EventHandler.OnEnemyChangeTarget += UpdateTarget;
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxHealth);
            resistance = Mathf.Clamp(resistance, 0, 1);
            
            if (health <= 0) Destroy(gameObject);

            BTree?.BTUpdate();
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
           
            if (otherGO.CompareTag("Projectile"))
            {
                var snowballScript = otherGO.GetComponent<BaseSnowball>();
                var damage = snowballScript.damage;
                if (snowballScript.type == SnowballType.RollingSnowball)
                {
                    damage *= otherGO.transform.localScale.x;
                }
                if (shield > 0)
                {
                    if (snowballScript.type == SnowballType.ThrowingSnowball)
                    {
                        damage *= 1f - resistance;
                    }

                    if (damage > shield)
                    {
                        var overflowDamage = damage - shield;
                        shield = 0;
                        health -= overflowDamage;
                    }
                    else
                    {
                        shield -= damage;
                    }
                }
                else
                {
                    health -= damage;
                }

                _playerAttr.energy += damage/5;
            }
        }
        
        protected virtual void UpdateTarget(GameObject tar)
        {
            target = tar;
            TargetTrans = target.GetComponent<Transform>();
        }

        protected virtual void SetUpBehaviorTree(){}

        protected bool IsOutOfCampRange()
        {
            var distance = Vector3.Distance(campTrans.position, transform.position);
            return distance > campRange;
        }
        
        protected bool IsInCampRange()
        {
            var distance = Vector3.Distance(campTrans.position, transform.position);
            return distance < campRange;
        }
        
        protected bool IsOutOfChaseRange()
        {
            var distance = Vector3.Distance(TargetTrans.position, transform.position);
            return distance > chaseRange;
        }

        protected void GoBackToCamp()
        {
            if (_agent != null && _agent.isActiveAndEnabled && campTrans != null)
            {
                _agent.SetDestination(_originalPosition);
            }
        }
        
        protected void SetNavigation()
        {
            if (_agent != null && _agent.isActiveAndEnabled && TargetTrans != null)
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

        protected void StartAttack()
        {
            _attackCoroutine ??= StartCoroutine(AttackCoroutine());
        }

        protected void StopAttack()
        {
            if (_attackCoroutine == null) return;
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        protected virtual IEnumerator AttackCoroutine()
        {
            return null;
        }
    }
}
