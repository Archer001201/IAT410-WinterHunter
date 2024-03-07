using System.Collections;
using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalBasicAttackState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicAttackCoroutine, CurrentEnemy.BasicAttack);
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExist()
        {
            CurrentEnemy.StopCurrentCoroutine(CurrentEnemy.BasicAttackCoroutine);
            CurrentEnemy.basicAttackTimer = CurrentEnemy.basicAttackCooldown;
        }
    }
}
