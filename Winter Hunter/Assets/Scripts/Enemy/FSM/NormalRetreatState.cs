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
            // Debug.Log("Retreat");
            if (CurrentEnemy.targetTrans != null)
            {
                CurrentEnemy.SwitchMovingState(MovingState.Chase);
            }
            CurrentEnemy.GoBackToCamp();
            // throw new System.NotImplementedException();
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
