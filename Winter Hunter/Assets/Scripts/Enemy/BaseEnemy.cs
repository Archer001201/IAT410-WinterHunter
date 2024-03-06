using System;
using System.Collections;
using Enemy.FSM;
using Snowman;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

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
        public float speed;
        public float attackRange;
        public float attackDamage;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        public bool isPlayerInCampRange;
        public bool isTaunted;
        public bool isChasing;
        [Header("Component Settings")]
        public GameObject hudCanvas;

        public Transform targetTrans;
        public Transform detectedSnowman;
        public Transform detectedPlayer;
        
        private NavMeshAgent _agent;
        private GameObject _player;
        private Coroutine _attackCoroutine;
        private Vector3 _originalPosition;

        private BaseState _currentState;
        protected BaseState IdleState;
        protected BaseState ChaseState;
        protected BaseState RetreatState;

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            
            _player = GameObject.FindWithTag("Player");

            _currentState = IdleState;
        }

        private void OnEnable()
        {
            _currentState.OnEnter(this);
        }

        private void OnDisable()
        {
            _currentState.OnExist();
        }

        private void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxHealth);

            if (health <= 0)
            {
                Destroy(gameObject);
                return;
            }

            if (isPlayerInCampRange && targetTrans == null)
            {
                isChasing = true;
                SetChaseTarget();
            }
            
            _currentState.OnUpdate();
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
            
            _currentState.OnExist();
            _currentState = newState;
            _currentState?.OnEnter(this);
        }

        public void SetTauntingTarget(Transform tar)
        {
            if (tar != null)
            {
                targetTrans = tar;
                isTaunted = true;
            }
            else
            {
                Debug.Log("set target null");
                isTaunted = false;
                targetTrans = null;
                // SetChaseTarget();
            }
        }

        public void SetChaseTarget()
        {
            if (isTaunted) return;
            var noDetectedTarget = detectedPlayer == null && detectedSnowman == null;
            if (isPlayerInCampRange && noDetectedTarget)
            {
                targetTrans = _player.transform;
                Debug.Log("no targets, chase player");
                // isChasing = true;
            }
            else if (noDetectedTarget)
            {
                GoBackToCamp();
                targetTrans = null;
                Debug.Log("no targets, back to camp");
                isChasing = false;
            }
            else if (isChasing)
            {
                if (detectedPlayer != null && detectedSnowman != null)
                {
                    var randNum = Random.Range(0f, 1f);
                    targetTrans = randNum > detectedSnowman.GetComponent<BaseSnowman>().aggro ? detectedPlayer : detectedSnowman;
                    Debug.Log("2 targets, wait for targeting" + randNum);
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
