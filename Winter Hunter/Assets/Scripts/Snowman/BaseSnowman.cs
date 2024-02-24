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
        public SnowmanSO snowmanSO;
        public float manaCost;
        public SnowmanLevel level;
        [Header("Dynamic Attributes")] 
        public float health;
        public float summonTimer;
        [Header("Component Settings")] 
        public GameObject hudCanvas;
        public List<Transform> detectedTargets;

        private Transform _targetTrans;
        private NavMeshAgent _agent;
        private float _startTime;
        

        protected virtual void Awake()
        {
            manaCost = snowmanSO.manaCost;

            health = snowmanSO.health;
            _startTime = Time.time;
            
            hudCanvas.SetActive(true);
            _targetTrans = GameObject.FindWithTag("Player").transform;
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
            health = Mathf.Clamp(health, 0, snowmanSO.health);
            summonTimer = Time.time - _startTime;

            if (health <= 0 || summonTimer >= snowmanSO.summonDuration)
            {
                Destroy(gameObject);
            }
            
            Move();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (snowmanSO.movementMode != MovementMode.ChaseEnemy && other.CompareTag("Enemy")) detectedTargets.Add(other.transform);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (snowmanSO.movementMode == MovementMode.ChaseEnemy && other.CompareTag("Enemy")) detectedTargets.Remove(other.transform);
        }
        
        private Transform FindClosestEnemy()
        {
            Transform closestTarget = null;
            var closestDistance = Mathf.Infinity;
            
            for (var i = detectedTargets.Count - 1; i >= 0; i--)
            {
                if (detectedTargets[i] == null)
                {
                    detectedTargets.RemoveAt(i);
                    continue;
                }

                var distance = Vector3.Distance(this.transform.position, detectedTargets[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = detectedTargets[i];
                }
            }
            return closestTarget != null ? closestTarget : transform;
        }
        
        private void Move()
        {
            switch (snowmanSO.movementMode)
            {
                case MovementMode.Stationary:
                    return;
                case MovementMode.ChaseEnemy:
                    _targetTrans = FindClosestEnemy();
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
            if (_agent.isActiveAndEnabled && _targetTrans != null)
            {
                _agent.SetDestination(_targetTrans.position);
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
    }
}
