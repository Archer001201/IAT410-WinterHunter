using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy
{
    public class FireRing : MonoBehaviour
    {
        private ParticleSystem _particle;
        private GameObject _player;
        private bool _hasDamaged;
        private float _lastParticleTime;
        private BasicEnemy _attr;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
            _attr = GetComponentInParent<BasicEnemy>();
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _particle.trigger.SetCollider(0, _player.GetComponent<Collider>());
        }

        private void Update()
        {
            if (_particle.time < _lastParticleTime)
            {
                _hasDamaged = false;
            }
            _lastParticleTime = _particle.time;
        }

        private void OnParticleTrigger()
        {
            if (_hasDamaged) return;
            
            var enter = new List<ParticleSystem.Particle>();
            var numEnter = _particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            if (numEnter > 0)
            {
                var playerAttr = _player.GetComponent<PlayerAttribute>();
                if (playerAttr != null)
                {
                    playerAttr.health -= _attr.attackDamage; 
                    _hasDamaged = true;
                }
            }
        }
    }
}

