using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalChaseState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
        }

        public override void OnUpdate()
        {
            // Debug.Log("Chase");
            if (CurrentEnemy.targetTrans == null)
            {
                CurrentEnemy.SwitchState(EnemyState.Retreat);
            }
            
            CurrentEnemy.StartMoving();
            CurrentEnemy.MoveTowardsTarget();
            CurrentEnemy.Attack();
        }

        public override void OnFixedUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnExist()
        {
            // throw new System.NotImplementedException();
        }
    }
}
