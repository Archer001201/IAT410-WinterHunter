using Player;
using Snowball;
using UnityEngine;
using BTFrame;
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
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        [Header("Component Settings")]
        public GameObject hudCanvas;
        public GameObject target;

        protected Transform TargetTrans;
        protected BehaviorTree BTree;
        protected NavMeshAgent Agent;
        
        private GameObject _player;
        private PlayerAttribute _playerAttr;
        

        protected virtual void Awake()
        {
            health = maxHealth;
            shield = maxShield;
            
            hudCanvas.SetActive(true);
            Agent = GetComponent<NavMeshAgent>();
            
            SetUpBehaviorTree();
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _playerAttr = _player.GetComponent<PlayerAttribute>();
            
            UpdateTarget(_player);
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

                _playerAttr.charge += damage;
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateTarget(GameObject tar)
        {
            target = tar;
            TargetTrans = target.GetComponent<Transform>();
        }

        protected virtual void SetUpBehaviorTree(){}
    }
}
