using System.Collections.Generic;
using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create Level_SO", fileName = "Level_SO")]
    public class LevelSO : ScriptableObject
    {
        public List<GameObject> enemyCamps;
    }
}
