using System;
using DataSO;
using Player;
using UnityEngine;

namespace Snowball
{
    /*
     * Super class of snowball
     */
    public class BaseSnowball : MonoBehaviour
    {
        public float attackFactor;
        public float damage;
        public SnowballType type;
        private PlayerSO _playerSO;

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            damage = attackFactor * _playerSO.attack;
        }
    }
}
