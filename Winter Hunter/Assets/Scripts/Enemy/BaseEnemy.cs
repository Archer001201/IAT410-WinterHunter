using System.Collections;
using Player;
using Snowball;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

namespace Enemy
{
    /*
     * Super class of enemy
     */
    public class BaseEnemy : MonoBehaviour
    {
        [Header("Static Attributes")]
        public float maxHealth;
        public float maxShield;
        public float resistance;
        public float attackingRange;
        public float attackDamage;
        public Transform campTrans;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        public bool isChasing;
        [Header("Component Settings")]
        public GameObject hudCanvas;
        public GameObject target;

        protected Transform TargetTrans;
        
        private NavMeshAgent _agent;
        private GameObject _player;
        private PlayerAttribute _playerAttr;
        private Coroutine _attackCoroutine;
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

            EventHandler.OnEnemyChangeTarget += UpdateTarget;
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxHealth);
            resistance = Mathf.Clamp(resistance, 0, 1);
            
            if (health <= 0) Destroy(gameObject);

            var distanceBetweenTarget = Vector3.Distance(TargetTrans.position, transform.position);
            var distanceBetweenOriginalPos = Vector3.Distance(_originalPosition, transform.position);
            
            if (distanceBetweenTarget > attackingRange || !isChasing) StopAttacking();
            else StartAttacking();
           
            if (isChasing)
            {
                StartMoving();
                MoveTowardsTarget();
            }
            else
            {
                if (distanceBetweenOriginalPos > 0.5f) GoBackToCamp();
                else StopMoving();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
           
            if (otherGO.CompareTag("Projectile"))
            {
                GetHurtFromSnowball(otherGO);
            }
        }
        
        /*
         * Assign a target game object to enemy
         */
        protected virtual void UpdateTarget(GameObject tar)
        {
            target = tar;
            TargetTrans = target.GetComponent<Transform>();
        }


        /*
         * Go back to camp
         */
        private void GoBackToCamp()
        {
            if (_agent != null && _agent.isActiveAndEnabled && campTrans != null)
            {
                _agent.SetDestination(_originalPosition);
            }
        }
        
        /*
         * Set target object's position as destination for NavMesh Agent
         */
        protected void MoveTowardsTarget()
        {
            if (_agent != null && _agent.isActiveAndEnabled && TargetTrans != null)
            {
                _agent.SetDestination(TargetTrans.position);
            }
        }

        /*
         * Start chasing, set NavMesh Agent continue to move
         */
        private void StartMoving()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (_agent.isStopped) _agent.isStopped = false;
        }

        /*
         * Stop chasing, set NavMesh Agent stop moving
         */
        private void StopMoving()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (!_agent.isStopped) _agent.isStopped = true;
        }

        /*
         * Start attacking, start attack coroutine
         */
        private void StartAttacking()
        {
            _attackCoroutine ??= StartCoroutine(AttackCoroutine());
        }

        /*
         * Stop attacking, stop and clear attack coroutine
         */
        private void StopAttacking()
        {
            if (_attackCoroutine == null) return;
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        /*
         * Attack coroutine
         */
        protected virtual IEnumerator AttackCoroutine()
        {
            return null;
        }

        /*
         * Calculate shield and health after snowball hit
         */
        private void GetHurtFromSnowball(GameObject otherGO)
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

            _playerAttr.mana += damage/5;
        }
    }
}
