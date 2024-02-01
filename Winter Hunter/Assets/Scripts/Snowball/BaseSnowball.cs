using System;
using Player;
using UnityEngine;

namespace Snowball
{
    public class BaseSnowball : MonoBehaviour
    {
        public float attackFactor;
        public float damage;
        public SnowballType type;
        private PlayerAttribute _playerAttr;

        private void Awake()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
            damage = attackFactor * _playerAttr.attack;
        }
    }
}
