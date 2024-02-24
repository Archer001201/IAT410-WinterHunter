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
        // private PlayerSO _playerSO;
        private PlayerAttribute _playerAttr;

        private void Awake()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
            // _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            damage = attackFactor * _playerAttr.attack;
        }
    }
}
