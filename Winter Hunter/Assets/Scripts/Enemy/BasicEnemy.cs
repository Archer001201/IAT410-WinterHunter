using Player;
using Snowball;
using UnityEngine;
using BTFrame;

namespace Enemy
{
    public class BasicEnemy : MonoBehaviour
    {
        [Header("Static Attributes")]
        public float maxHealth;
        public float maxShield;
        public float resistance;
        [Header("Dynamic Attributes")]
        public float health;
        public float shield;
        [Header("Component Settings")]
        public GameObject hudCanvas;

        private PlayerAttribute _playerAttr;
        protected BehaviorTree BTree;

        private void Awake()
        {
            health = maxHealth;
            shield = maxShield;
            
            hudCanvas.SetActive(true);
            
            SetUpBehaviorTree();
        }

        private void Start()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
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
                var snowballScript = otherGO.GetComponent<BasicSnowball>();
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

        protected virtual void SetUpBehaviorTree(){}
    }
}
