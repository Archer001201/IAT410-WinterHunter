using System;
using System.Collections;
using Player;
using Snowball;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utilities;

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
        // public float resistance;
        public float speed;
        public float attackRange;
        public float attackDamage;
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
        private Coroutine _attackCoroutine;
        private Vector3 _originalPosition;
        

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            
            _player = GameObject.FindWithTag("Player");
            
            SetTarget(_player);
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxHealth);
            // resistance = Mathf.Clamp(resistance, 0, 1)

            if (health <= 0)
            {
                Destroy(gameObject);
                return;
            }

            if (TargetTrans == null) TargetTrans = _player.transform;
            var distanceBetweenTarget = Vector3.Distance(TargetTrans.position, transform.position);
            var distanceBetweenOriginalPos = Vector3.Distance(_originalPosition, transform.position);
            
            if (distanceBetweenTarget > attackRange || !isChasing) StopAttacking();
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
        
        /*
         * Assign a target game object to enemy
         */
        public virtual void SetTarget(GameObject tar)
        {
            target = tar;
            TargetTrans = target.transform;
        }


        /*
         * Go back to camp
         */
        private void GoBackToCamp()
        {
            if (_agent != null && _agent.isActiveAndEnabled)
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

        public void TakeDamage(float damage, ShieldBreakEfficiency shieldBreakEfficiency)
        {
            if (shield > 0)
            {
                damage *= shieldBreakEfficiency switch
                {
                    ShieldBreakEfficiency.Low => 0.2f,
                    ShieldBreakEfficiency.Median => 0.6f,
                    ShieldBreakEfficiency.High => 1f,
                    _ => throw new ArgumentOutOfRangeException(nameof(shieldBreakEfficiency), shieldBreakEfficiency,
                        null)
                };

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
        }

        public void Slowdown(float originalSpeed, float slowRate, float duration)
        {
            StartCoroutine(SlowdownCoroutine(originalSpeed, slowRate, duration));
        }

        private IEnumerator SlowdownCoroutine(float originalSpeed, float slowRate, float duration)
        {
            _agent.speed = originalSpeed * slowRate;
            yield return new WaitForSeconds(duration);
            _agent.speed = originalSpeed;
        }
    }
}
