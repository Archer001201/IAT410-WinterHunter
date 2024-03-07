using System;
using System.Collections;
using Enemy.FSM;
using Player;
using Snowman;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utilities;
using Debug = UnityEngine.Debug;
using EventHandler = Utilities.EventHandler;
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
        public float basicAttackCooldown;
        public float basicSkillCooldown;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        public bool isPlayerInCampRange;
        public bool isTaunted;
        public bool isChasing;
        public float basicAttackTimer;
        public float basicSkillTimer;
        public bool isBasicAttackSatisfied;
        public bool isBasicSkillSatisfied;
        public bool isBasicAttackReady;
        public bool isBasicSkillReady;
        [Header("Component Settings")]
        public GameObject hudCanvas;

        public Transform targetTrans;
        public BaseSnowman detectedSnowman;
        public PlayerAttribute detectedPlayer;
        
        public NavMeshAgent agent;
        private GameObject _player;
        // private Coroutine _attackCoroutine;
        private Vector3 _originalPosition;

        private BaseState _currentMovingState;
        protected BaseState IdleState;
        protected BaseState ChaseState;
        protected BaseState RetreatState;

        private BaseState _currentAttackingState;
        protected BaseState NonAttackState;
        protected BaseState BasicAttackState;
        protected BaseState BasicSkillState;

        public Coroutine BasicAttackCoroutine;
        public Coroutine BasicSkillCoroutine;

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            
            _player = GameObject.FindWithTag("Player");

            _currentMovingState = IdleState;
            _currentAttackingState = NonAttackState;
        }

        private void OnEnable()
        {
            _currentMovingState.OnEnter(this);
            _currentAttackingState.OnEnter(this);
            // StartAttacking();
        }

        private void OnDisable()
        {
            _currentMovingState.OnExist();
            _currentAttackingState.OnExist();
        }

        protected virtual void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxHealth);
            basicAttackTimer = Mathf.Clamp(basicAttackTimer, 0, basicAttackCooldown);
            basicSkillTimer = Mathf.Clamp(basicSkillTimer, 0, basicSkillCooldown);
            
            if (health <= 0)
            {
                EventHandler.RemoveEnemyToCombatList(gameObject);
                Destroy(gameObject);
                return;
            }

            if (isPlayerInCampRange && targetTrans == null)
            {
                isChasing = true;
                SetChaseTarget();
            }

            if (targetTrans == null && Vector3.Distance(transform.position, _originalPosition) < 0.5f)
            {
                isChasing = false;
            }

            isBasicAttackReady = basicAttackTimer <= 0 && isBasicAttackSatisfied && targetTrans != null;
            isBasicSkillReady = basicSkillTimer <= 0 && isBasicSkillSatisfied && targetTrans != null; 
            
            _currentMovingState.OnUpdate();
            _currentAttackingState.OnUpdate();
        }

        private void FixedUpdate()
        {
            _currentMovingState.OnFixedUpdate();
            if (basicAttackTimer > 0) basicAttackTimer -= Time.fixedDeltaTime;
            if (basicSkillTimer > 0) basicSkillTimer -= Time.fixedDeltaTime;
        }

        public void SwitchMovingState(MovingState state)
        {
            var newState = state switch
            {
                MovingState.Chase => ChaseState,
                MovingState.Retreat => RetreatState,
                MovingState.Idle => IdleState,
                _ => null
            };
            
            _currentMovingState.OnExist();
            _currentMovingState = newState;
            _currentMovingState?.OnEnter(this);
        }
        
        public void SwitchAttackingState(AttackingState state)
        {
            var newState = state switch
            {
                AttackingState.NonAttack => NonAttackState,
                AttackingState.BasicAttack => BasicAttackState,
                AttackingState.BasicSkill => BasicSkillState,
                _ => null
            };
            
            _currentAttackingState.OnExist();
            _currentAttackingState = newState;
            _currentAttackingState?.OnEnter(this);
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
                // Debug.Log("no targets, chase player");
                // isChasing = true;
            }
            else if (noDetectedTarget)
            {
                GoBackToCamp();
                targetTrans = null;
                // Debug.Log("no targets, back to camp");
                // isChasing = false;
            }
            else if (isChasing)
            {
                if (detectedPlayer != null && detectedSnowman != null)
                {
                    var randNum = Random.Range(0f, 1f);
                    targetTrans = randNum > detectedSnowman.aggro ? _player.transform : detectedSnowman.transform;
                    // Debug.Log("2 targets, wait for targeting" + randNum);
                }
                else if (detectedSnowman != null)
                {
                    targetTrans = detectedSnowman.transform;
                    // Debug.Log("1 target, snowman");
                }
                else
                {
                    targetTrans = _player.transform;
                    // Debug.Log("1 target, player");
                }
            }
        }


        /*
         * Go back to camp
         */
        public void GoBackToCamp()
        {
            if (agent != null && agent.isActiveAndEnabled)
            {
                agent.SetDestination(_originalPosition);
            }
        }
        
        /*
         * Set target object's position as destination for NavMesh Agent
         */
        public void MoveTowardsTarget()
        {
            if (agent != null && agent.isActiveAndEnabled && targetTrans != null)
            {
                agent.SetDestination(targetTrans.position);
            }
        }

        /*
         * Start chasing, set NavMesh Agent continue to move
         */
        public void StartMoving()
        {
            if (!agent.isActiveAndEnabled) return;
            if (agent.isStopped) agent.isStopped = false;
        }

        /*
         * Stop chasing, set NavMesh Agent stop moving
         */
        public void StopMoving()
        {
            if (!agent.isActiveAndEnabled) return;
            if (!agent.isStopped) agent.isStopped = true;
        }

        // public void Attack()
        // {
        //     if (targetTrans == null) return;
        //     // var distanceBetweenTarget = Vector3.Distance(targetTrans.position, transform.position);
        //     //
        //     // if (distanceBetweenTarget > attackRange) StopAttacking();
        //     // else 
        //         StartAttacking();
        // }

        /*
         * Start attacking, start attack coroutine
         */
        // protected void StartAttacking()
        // {
        //     if (targetTrans == null) return;
        //     _attackCoroutine ??= StartCoroutine(AttackCoroutine());
        // }
        //
        // /*
        //  * Stop attacking, stop and clear attack coroutine
        //  */
        // protected void StopAttacking()
        // {
        //     if (_attackCoroutine == null) return;
        //     StopCoroutine(_attackCoroutine);
        //     _attackCoroutine = null;
        // }
        //
        // /*
        //  * Attack coroutine
        //  */
        // protected virtual IEnumerator AttackCoroutine()
        // {
        //     return null;
        // }

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
            agent.speed = originalSpeed * slowRate;
            yield return new WaitForSeconds(duration);
            agent.speed = originalSpeed;
        }

        public virtual IEnumerator BasicAttack()
        {
            return null;
        }

        public void StartCurrentCoroutine(Coroutine currentCoroutine, Func<IEnumerator> func)
        {
            currentCoroutine ??= StartCoroutine(func());
        }

        public virtual IEnumerator BasicSkill()
        {
            return null;
        }

        // public void StartBasicSkillCoroutine()
        // {
        //     BasicSkillCoroutine ??= StartCoroutine()
        // }

        public void StopCurrentCoroutine(Coroutine currentCoroutine)
        {
            if (currentCoroutine == null) return;
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }
}
