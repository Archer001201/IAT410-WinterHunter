using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalChaseState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            EventHandler.AddEnemyToCombatList(CurrentEnemy.gameObject);
        }

        public override void OnUpdate()
        {
            if (CurrentEnemy.targetTrans == null)
            {
                CurrentEnemy.SwitchMovingState(MovingState.Retreat);
            }
            
            CurrentEnemy.StartMoving();
            CurrentEnemy.MoveTowardsTarget();
        }

        public override void OnFixedUpdate()
        {
            
        }

        public override void OnExist()
        {
            EventHandler.RemoveEnemyToCombatList(CurrentEnemy.gameObject);
        }
    }
}
