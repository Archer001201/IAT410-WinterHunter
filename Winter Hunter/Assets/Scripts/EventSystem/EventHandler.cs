using System;
using System.Collections.Generic;
using Snowman;
using UnityEngine;
using Utilities;

namespace EventSystem
{
    /*
     * Observer pattern
     */
    public static class EventHandler
    {
        /*
         * Notice enemy to change target
         */
        public static event Action<GameObject> OnEnemyChangeTarget;

        public static void EnemyChangeTarget(GameObject target)
        {
            OnEnemyChangeTarget?.Invoke(target);
        }

        /*
         * Notice existed snowman to destroy itself
         */
        public static event Action OnDestroyExistedSnowman;

        public static void DestroyExistedSnowman()
        {
            OnDestroyExistedSnowman?.Invoke();
        }
        
        /*
         * Notice player a snowman chest has been opened
         */
        public static event Action<List<Enums.SnowmanType>> OnOpenSnowmanChest;

        public static void OpenSnowmanChest(List<Enums.SnowmanType> snowmanList)
        {
            OnOpenSnowmanChest?.Invoke(snowmanList);
        }

        /*
         * Notice skill panel to update ui display
         */
        public static event Action OnUpdateSkillPanel;

        public static void UpdateSkillPanel()
        {
            OnUpdateSkillPanel?.Invoke();
        }

        /*
         * Notice game player has been died
         */
        public static event Action OnPlayerDie;

        public static void PlayerDie()
        {
            OnPlayerDie?.Invoke();
        }
    }
}
