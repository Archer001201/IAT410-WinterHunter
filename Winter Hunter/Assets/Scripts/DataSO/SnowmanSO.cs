using Snowman;
using UnityEngine;
using UnityEngine.UI;

namespace DataSO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create Snowman_SO", fileName = "Snowman_SO")]
    public class SnowmanSO : ScriptableObject
    {
        public SnowmanType type;
        public GameObject snowmanPrefab;
        public Sprite snowmanIcon;
        public float maxHealth;
        public float summoningTime;
        public float cooldown;
        public float attack;
        public float attackRate;
        public float summoningCost;
        [TextArea]
        public string description;
    }
}
