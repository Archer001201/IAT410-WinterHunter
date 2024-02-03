using Snowman;
using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create Snowman_SO", fileName = "Snowman_SO")]
    public class SnowmanSO : ScriptableObject
    {
        public SnowmanType type;
        public GameObject snowmanPrefab;
        public float maxHealth;
        public float summoningTime;
        public float cooldown;
        public float attack;
        public float attackRate;
        public float summoningCost;
    }
}
