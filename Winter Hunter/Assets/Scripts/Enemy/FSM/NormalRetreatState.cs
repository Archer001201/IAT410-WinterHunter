using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalRetreatState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
        }

        public override void OnUpdate()
        {
            if (CurrentEnemy.targetTrans != null)
            {
                CurrentEnemy.SwitchMovingState(MovingState.Chase);
            }
            
            if (!CurrentEnemy.isChasing) CurrentEnemy.SwitchMovingState(MovingState.Idle);
            CurrentEnemy.GoBackToCamp();
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExist()
        {

        }
    }
}
