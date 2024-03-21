using UnityEngine;
using Utilities;

namespace Enemy.AnimatorControllers
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        public BaseEnemy enemy;

        public void Attack()
        {
            enemy.CurrentAttackingState.OnCall();
        }

        public void Death()
        {
            enemy.Death();
        }
    }
}
