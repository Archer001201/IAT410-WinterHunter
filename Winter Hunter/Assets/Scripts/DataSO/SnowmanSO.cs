using Snowman;
using UnityEngine;
using Utilities;

namespace DataSO
{
    /*
     * Store snowman's attributes
     */
    [CreateAssetMenu(menuName = "ScriptableObject/Create Snowman_SO", fileName = "Snowman_SO")]
    public class SnowmanSO : ScriptableObject
    {
        public Enums.SnowmanType type;
        public GameObject prefab;
        public Sprite icon;
        public float health;
        public float summonDuration;
        public float cooldown;
        public float attack;
        public float attackSpeed;
        public float manaCost;
        [TextArea]
        public string description;
    }
}
