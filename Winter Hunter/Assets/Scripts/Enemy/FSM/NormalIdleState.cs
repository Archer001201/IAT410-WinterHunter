using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalIdleState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
        }

        public override void OnUpdate()
        {
            // Debug.Log("Idle");
            if (CurrentEnemy.targetTrans != null)
            {
                CurrentEnemy.SwitchMovingState(MovingState.Chase);
            }
            // if (CurrentEnemy.isPlayerInCampRange)
            // {
            //     // if (CurrentEnemy.targetTransList.Count < 2) CurrentEnemy.targetTrans = CurrentEnemy.player.transform;
            //     // else
            //     // {
            //     //     
            //     // }
            // }
            // throw new System.NotImplementedException();
            // var distanceBetweenTarget =
            //     Vector3.Distance(CurrentEnemy.transform.position, CurrentEnemy.targetTrans.position);
            //
            // if (CurrentEnemy.isChasing) CurrentEnemy.SwitchState(EnemyState.Chase);
        }

        public override void OnFixedUpdate()
        {
            // throw new System.NotImplementedException();
            if (CurrentEnemy.isChasing) return;
            if (CurrentEnemy.health < CurrentEnemy.maxHealth) CurrentEnemy.health += 5 * Time.fixedDeltaTime;
            if (CurrentEnemy.shield < CurrentEnemy.maxShield) CurrentEnemy.shield += 5 * Time.fixedDeltaTime;
        }

        public override void OnExist()
        {
            // throw new System.NotImplementedException();
        }
    }
}
