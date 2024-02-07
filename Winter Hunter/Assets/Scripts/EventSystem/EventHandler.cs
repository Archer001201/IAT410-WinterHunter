using System;
using System.Collections.Generic;
using Snowman;
using UnityEngine;

namespace EventSystem
{
    public static class EventHandler
    {
        public static event Action<GameObject> OnEnemyChangeTarget;

        public static void EnemyChangeTarget(GameObject target)
        {
            OnEnemyChangeTarget?.Invoke(target);
        }

        public static event Action OnDestroyExistedSnowman;

        public static void DestroyExistedSnowman()
        {
            OnDestroyExistedSnowman?.Invoke();
        }

        public static event Action<List<SnowmanType>> OnOpenSnowmanChest;

        public static void OpenSnowmanChest(List<SnowmanType> snowmanList)
        {
            OnOpenSnowmanChest?.Invoke(snowmanList);
        }

        public static event Action OnUpdateSkillPanel;

        public static void UpdateSkillPanel()
        {
            OnUpdateSkillPanel?.Invoke();
        }
    }
}
