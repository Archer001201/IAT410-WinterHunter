using System;
using System.Collections.Generic;
using DataSO;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using EventHandler = EventSystem.EventHandler;

namespace Snowman
{
    /*
     * Super class of snowman
     */
    public class BaseSnowman : MonoBehaviour
    {
        [Header("Static Attributes")] 
        public SnowmanType type;
        public float manaCost;
        public SnowmanLevel level;
        [Header("Dynamic Attributes")] 
        public float health;
        public float summonTimer;
        [Header("Component Settings")] 
        public GameObject hudCanvas;
        public List<Transform> detectedEnemies;

        protected SnowmanSO MySnowmanSO;
        protected Transform TargetTrans;
        private NavMeshAgent _agent;
        private float _startTime;
        

        protected virtual void Awake()
        {
            MySnowmanSO = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + type + "_SO");
            manaCost = MySnowmanSO.manaCost;

            health = MySnowmanSO.health;
            _startTime = Time.time;
            
            hudCanvas.SetActive(true);
            TargetTrans = GameObject.FindWithTag("Player").transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            EventHandler.OnDestroyExistedSnowman += DestroyMe;
        }

        private void OnDisable()
        {
            EventHandler.OnDestroyExistedSnowman -= DestroyMe;
        }

        protected virtual void Update()
        {
            health = Mathf.Clamp(health, 0, MySnowmanSO.health);
            summonTimer = Time.time - _startTime;

            if (health <= 0 || summonTimer >= MySnowmanSO.summonDuration)
            {
                DestroyMe();
            }
            
            Move();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")) detectedEnemies.Add(other.transform);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy")) detectedEnemies.Remove(other.transform);
        }
        
        private Transform FindClosestEnemy()
        {
            Transform closestTarget = null;
            var closestDistance = Mathf.Infinity;
            
            for (var i = detectedEnemies.Count - 1; i >= 0; i--)
            {
                if (detectedEnemies[i] == null)
                {
                    detectedEnemies.RemoveAt(i);
                    continue;
                }

                var distance = Vector3.Distance(this.transform.position, detectedEnemies[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = detectedEnemies[i];
                }
            }
            return closestTarget != null ? closestTarget : transform;
        }
        
        private void Move()
        {
            switch (MySnowmanSO.movementMode)
            {
                case MovementMode.Stationary:
                    return;
                case MovementMode.ChaseEnemy:
                    TargetTrans = FindClosestEnemy();
                    break;
                case MovementMode.FollowPlayer:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SetNavigation();
        }
        
        /*
         * Set target's position as destination for NavMesh Agent
         */
        private void SetNavigation()
        {
            if (_agent.isActiveAndEnabled && TargetTrans != null)
            {
                _agent.SetDestination(TargetTrans.position);
            }
        }

        /*
         * Destroy this game object
         */
        protected virtual void DestroyMe()
        {
            Destroy(gameObject);
        }

        public void SetLevel(SnowmanLevel snowmanLevel)
        {
            level = snowmanLevel;
        }

        public virtual void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}
