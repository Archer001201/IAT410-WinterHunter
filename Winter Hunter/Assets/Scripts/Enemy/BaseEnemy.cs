using System;
using System.Collections;
using Enemy.FSM;
using Player;
using Snowman;
using UISystem;
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
        public float advancedSkillCooldown;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        // public bool isPlayerInCampRange;
        public bool isTaunted;
        public bool isChasing;
        public float basicAttackTimer;
        public float basicSkillTimer;
        public float advancedSkillTimer;
        public bool isBasicAttackSatisfied;
        public bool isBasicSkillSatisfied;
        public bool isAdvancedSkillSatisfied;
        public bool isBasicAttackReady;
        public bool isBasicSkillReady;
        public bool isAdvancedSkillReady;
        public bool isMarked;
        [Header("Component Settings")]
        public GameObject hudCanvas;

        public Transform targetTrans;
        public BaseSnowman detectedSnowman;
        // public PlayerAttribute detectedPlayer;
        
        public NavMeshAgent agent;
        private GameObject _player;
        // private Coroutine _attackCoroutine;
        private Vector3 _originalPosition;

        // private BaseState _currentMovingState;
        // protected BaseState IdleState;
        // protected BaseState ChaseState;
        // protected BaseState RetreatState;

        public BaseState CurrentAttackingState;
        protected BaseState NonAttackState;
        protected BaseState BasicAttackState;
        protected BaseState BasicSkillState;
        protected BaseState AdvancedSkillState;

        public Coroutine BasicAttackCoroutine;
        public Coroutine BasicSkillCoroutine;
        public Coroutine AdvancedSkillCoroutine;

        public Animator animator;
        private bool _isDead;

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;

            _originalPosition = transform.position;
            
            hudCanvas.SetActive(true);
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            
            _player = GameObject.FindWithTag("Player");

            // _currentMovingState = IdleState;
            CurrentAttackingState = NonAttackState;
        }

        private void OnEnable()
        {
            // _currentMovingState.OnEnter(this);
            CurrentAttackingState?.OnEnter(this);
            // StartAttacking();
        }

        private void OnDisable()
        {
            // _currentMovingState.OnExist();
            CurrentAttackingState?.OnExist();
        }

        protected virtual void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
            shield = Mathf.Clamp(shield, 0, maxShield);
            basicAttackTimer = Mathf.Clamp(basicAttackTimer, 0, basicAttackCooldown);
            basicSkillTimer = Mathf.Clamp(basicSkillTimer, 0, basicSkillCooldown);
            advancedSkillTimer = Mathf.Clamp(advancedSkillTimer, 0, advancedSkillCooldown);
            
            if (health <= 0 && !_isDead)
            {
                _isDead = true;
                agent.speed = 0;
                animator.SetTrigger(EnemyAnimatorPara.IsDead.ToString());
                EventHandler.RemoveEnemyToCombatList(gameObject);
            }
            
            if (agent != null && agent.isActiveAndEnabled && targetTrans != null)
            {
                agent.SetDestination(targetTrans.position);
            }

            // if (isPlayerInCampRange && targetTrans == null)
            // {
            //     isChasing = true;
            //     SetChaseTarget();
            // }
            //
            // if (targetTrans == null && Vector3.Distance(transform.position, _originalPosition) < 0.5f)
            // {
            //     isChasing = false;
            // }

            isBasicAttackReady = basicAttackTimer <= 0 && isBasicAttackSatisfied && targetTrans != null;
            isBasicSkillReady = basicSkillTimer <= 0 && isBasicSkillSatisfied && targetTrans != null;
            isAdvancedSkillReady = advancedSkillTimer <= 0 && isAdvancedSkillSatisfied && targetTrans != null;

            // _currentMovingState.OnUpdate();
            CurrentAttackingState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (basicAttackTimer > 0) basicAttackTimer -= Time.fixedDeltaTime;
            if (basicSkillTimer > 0) basicSkillTimer -= Time.fixedDeltaTime;
            if (advancedSkillTimer > 0) advancedSkillTimer -= Time.deltaTime;
            // _currentMovingState.OnFixedUpdate();
            CurrentAttackingState?.OnFixedUpdate();
        }

        public void Death()
        {
            Destroy(gameObject);
        }

        // public void SwitchMovingState(MovingState state)
        // {
        //     var newState = state switch
        //     {
        //         MovingState.Chase => ChaseState,
        //         MovingState.Retreat => RetreatState,
        //         MovingState.Idle => IdleState,
        //         _ => IdleState
        //     };
        //     
        //     _currentMovingState.OnExist();
        //     _currentMovingState = newState;
        //     _currentMovingState?.OnEnter(this);
        // }
        
        public void SwitchAttackingState(AttackingState state)
        {
            var newState = state switch
            {
                AttackingState.NonAttack => NonAttackState,
                AttackingState.BasicAttack => BasicAttackState,
                AttackingState.BasicSkill => BasicSkillState,
                AttackingState.AdvancedSkill => AdvancedSkillState,
                _ => NonAttackState
            };
            
            CurrentAttackingState.OnExist();
            CurrentAttackingState = newState;
            CurrentAttackingState?.OnEnter(this);
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
                // Debug.Log("set target null");
                isTaunted = false;
                // SetChaseTarget();
            }
        }

        // public void ChasingTarget()
        // {
        //     
        // }

        public void SetTarget()
        {
            if (detectedSnowman != null)
            {
                if (Random.Range(0f, 1f) < detectedSnowman.aggro)
                {
                    targetTrans = detectedSnowman.transform;
                    return;
                }
            }
            if (_player == null) _player = GameObject.FindWithTag("Player");
            targetTrans = _player.transform;
        }

        // public void SetChaseTarget()
        // {
        //     if (isTaunted) return;
        //     // var noDetectedTarget = detectedPlayer == null && detectedSnowman == null;
        //     // if (isPlayerInCampRange && noDetectedTarget)
        //     // {
        //     //     if (_player == null) _player = GameObject.FindWithTag("Player");
        //     //     targetTrans = _player.transform;
        //     // }
        //     // else if (noDetectedTarget)
        //     // {
        //     //     GoBackToCamp();
        //     //     targetTrans = null;
        //     // }
        //     // else 
        //     if (isPlayerInCampRange)
        //     {
        //         // detectedPlayer = _player;
        //         _player = GameObject.FindWithTag("Player");
        //         if (detectedSnowman != null)
        //         {
        //             var randNum = Random.Range(0f, 1f);
        //             targetTrans = randNum > detectedSnowman.aggro ? _player.transform : detectedSnowman.transform;
        //         }
        //         // else if (detectedSnowman != null)
        //         // {
        //         //     targetTrans = detectedSnowman.transform;
        //         // }
        //         else
        //         {
        //             // if (_player == null) _player = GameObject.FindWithTag("Player");
        //             targetTrans = _player.transform;
        //         }
        //     }
        // }


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
            targetTrans = _player.transform;
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

        public void TakeDamage(float damage, ShieldBreakEfficiency shieldBreakEfficiency)
        {
            if (shield > 0)
            {
                damage *= shieldBreakEfficiency switch
                {
                    ShieldBreakEfficiency.Low => 0.05f,
                    ShieldBreakEfficiency.Median => 0.5f,
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

            if (isMarked) SetTargetSign(false);
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

        public virtual IEnumerator AdvancedSkill()
        {
            return null;
        }

        public void StopCurrentCoroutine(Coroutine currentCoroutine)
        {
            if (currentCoroutine == null) return;
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        public void SetTargetSign(bool isTargeted)
        {
            hudCanvas.GetComponent<EnemyHUD>().SetTargetSign(isTargeted);
            isMarked = isTargeted;
        }
    }
}
