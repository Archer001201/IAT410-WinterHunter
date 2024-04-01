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
        public bool isChasing;
        public bool isTaunted;
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
        // public GameObject deathVfx;

        public Transform targetTrans;
        public BaseSnowman detectedSnowman;
        
        public NavMeshAgent agent;
        private GameObject _player;

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
            
            hudCanvas.SetActive(true);
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            
            _player = GameObject.FindWithTag("Player");

            CurrentAttackingState = NonAttackState;
        }

        private void OnEnable()
        {
            CurrentAttackingState?.OnEnter(this);
        }

        private void OnDisable()
        {
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
                agent.isStopped = true;
                animator.SetTrigger(EnemyAnimatorPara.IsDead.ToString());
                EventHandler.RemoveEnemyToCombatList(gameObject);
                // Instantiate(deathVfx, transform.position, Quaternion.identity);
            }
            
            if (agent != null && agent.isActiveAndEnabled && targetTrans != null && isChasing)
            {
                agent.SetDestination(targetTrans.position);
                animator.SetBool(EnemyAnimatorPara.IsMoving.ToString(), true);
            }
            
            if (targetTrans == null) SetTarget();

            isBasicAttackReady = basicAttackTimer <= 0 && isBasicAttackSatisfied && targetTrans != null;
            isBasicSkillReady = basicSkillTimer <= 0 && isBasicSkillSatisfied && targetTrans != null;
            isAdvancedSkillReady = advancedSkillTimer <= 0 && isAdvancedSkillSatisfied && targetTrans != null;
            CurrentAttackingState?.OnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (basicAttackTimer > 0) basicAttackTimer -= Time.fixedDeltaTime;
            if (basicSkillTimer > 0) basicSkillTimer -= Time.fixedDeltaTime;
            if (advancedSkillTimer > 0) advancedSkillTimer -= Time.deltaTime;
            CurrentAttackingState?.OnFixedUpdate();
        }

        public void Death()
        {
            Destroy(gameObject);
        }
        
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
                isTaunted = false;
                SetTarget();
            }
        }

        public void SetTarget()
        {
            if (isTaunted) return;
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
