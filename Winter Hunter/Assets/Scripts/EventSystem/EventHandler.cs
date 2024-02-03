using System;
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
    }
}
