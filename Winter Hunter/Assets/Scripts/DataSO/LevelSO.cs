using System.Collections.Generic;
using Enemy;
using Props;
using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create Level_SO", fileName = "Level_SO")]
    public class LevelSO : ScriptableObject
    {
        public List<CampData> enemyCamps;
        public List<TreasureChestData> treasureChests;
        public string sceneName;
        public Vector3 position;
        public bool respawnAtThisPosition;
    }
}
