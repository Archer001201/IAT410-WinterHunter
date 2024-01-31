using System;
using UnityEngine;

namespace EventSystem
{
    public static class EventHandler
    {
        // public static event Action<GameObject> OnSummonSnowman;
        //
        // public static void SummonSnowman(GameObject snowman)
        // {
        //     OnSummonSnowman?.Invoke(snowman);
        // }

        public static event Action<GameObject> OnEnemyChangeTarget;

        public static void EnemyChangeTarget(GameObject target)
        {
            OnEnemyChangeTarget?.Invoke(target);
        }
    }
}
