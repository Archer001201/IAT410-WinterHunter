using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Enemy.FSM;
using Player;
using Snowball;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utilities;
using Debug = UnityEngine.Debug;

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
        public bool isPlayerInCampRange;
        [Header("Component Settings")]
        public GameObject hudCanvas;
        // public GameObject target;

        public Transform targetTrans;
        // public List<Transform> targetTransList;
        public Transform detectedSnowman;
        public Transform detectedPlayer;
        
        private NavMeshAgent _agent;
        public GameObject player;
        private Coroutine _attackCoroutine;
        private Vector3 _originalPosition;

        public BaseState CurrentState;
        protected BaseState IdleState;
        public BaseState ChaseState;
        protected BaseState RetreatState;

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            
            player = GameObject.FindWithTag("Player");
            // SetTarget(_player.transform);

            CurrentState = IdleState;
        }

        private void OnEnable()
        {
            CurrentState.OnEnter(this);
        }

        private void OnDisable()
        {
            CurrentState.OnExist();
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

            // if (targetTrans == null) targetTrans = _player.transform;
            // var distanceBetweenTarget = Vector3.Distance(TargetTrans.position, transform.position);
            // var distanceBetweenOriginalPos = Vector3.Distance(_originalPosition, transform.position);
            //
            // if (distanceBetweenTarget > attackRange || !isChasing) StopAttacking();
            // else StartAttacking();
            //
            // if (isChasing)
            // {
            //     StartMoving();
            //     MoveTowardsTarget();
            // }
            // else
            // {
            //     if (distanceBetweenOriginalPos > 0.5f) GoBackToCamp();
            //     else StopMoving();
            // }

            // if (targetTrans != null)
            // {
            //     var distanceBetweenTarget = Vector3.Distance(targetTrans.position, transform.position);
            //     // var distanceBetweenOriginalPos = Vector3.Distance(_originalPosition, transform.position);
            //     
            //     if (distanceBetweenTarget > attackRange) StopAttacking();
            //     else StartAttacking();
            //     
            //     StartMoving();
            //     MoveTowardsTarget();
            //     
            //     // if (isChasing)
            //     // {
            //     //     StartMoving();
            //     //     MoveTowardsTarget();
            //     // }
            //     // else
            //     // {
            //     //     if (distanceBetweenOriginalPos > 0.5f) GoBackToCamp();
            //     //     else StopMoving();
            //     // }
            // }
            
            CurrentState.OnUpdate();
        }

        private void FixedUpdate()
        {
            // CurrentState.OnFixedUpdate();
        }

        public void SwitchState(EnemyState state)
        {
            var newState = state switch
            {
                EnemyState.Chase => ChaseState,
                EnemyState.Retreat => RetreatState,
                _ => null
            };
            
            CurrentState.OnExist();
            CurrentState = newState;
            CurrentState?.OnEnter(this);
        }
        
        // private void CheckPlayerInRaycastRange()
        // {
        //     var dir = _player.transform.position - transform.position;
        //
        //     var layerMask =  1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Player");
        //
        //
        //     
        //     Debug.DrawLine(transform.position, _player.transform.position, Color.blue);
        // }

        /*
         * Assign a target game object to enemy
         */
        public virtual void SetTarget(Transform tar)
        {
            // target = tar;
            targetTrans = tar;
        }

        public void SetChaseTarget()
        {
            var noDetectedTarget = detectedPlayer == null && detectedSnowman == null;
            if (isPlayerInCampRange && noDetectedTarget)
            {
                targetTrans = player.transform;
                Debug.Log("no targets, chase player");
            }
            else if (noDetectedTarget)
            {
                GoBackToCamp();
                targetTrans = null;
                Debug.Log("no targets, back to camp");
            }
            else
            {
                if (detectedPlayer != null && detectedSnowman != null)
                {
                    targetTrans = detectedPlayer;
                    Debug.Log("2 targets, wait for targeting");
                }
                else if (detectedSnowman != null)
                {
                    targetTrans = detectedSnowman;
                    Debug.Log("1 target, snowman");
                }
                else
                {
                    targetTrans = detectedPlayer;
                    Debug.Log("1 target, player");
                }
            }
        }


        /*
         * Go back to camp
         */
        public void GoBackToCamp()
        {
            if (_agent != null && _agent.isActiveAndEnabled)
            {
                _agent.SetDestination(_originalPosition);
            }
        }
        
        /*
         * Set target object's position as destination for NavMesh Agent
         */
        public void MoveTowardsTarget()
        {
            if (_agent != null && _agent.isActiveAndEnabled && targetTrans != null)
            {
                _agent.SetDestination(targetTrans.position);
            }
        }

        /*
         * Start chasing, set NavMesh Agent continue to move
         */
        public void StartMoving()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (_agent.isStopped) _agent.isStopped = false;
        }

        /*
         * Stop chasing, set NavMesh Agent stop moving
         */
        public void StopMoving()
        {
            if (!_agent.isActiveAndEnabled) return;
            if (!_agent.isStopped) _agent.isStopped = true;
        }

        public void Attack()
        {
            if (targetTrans == null) return;
            var distanceBetweenTarget = Vector3.Distance(targetTrans.position, transform.position);
            
            if (distanceBetweenTarget > attackRange) StopAttacking();
            else StartAttacking();
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
        public void StopAttacking()
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
