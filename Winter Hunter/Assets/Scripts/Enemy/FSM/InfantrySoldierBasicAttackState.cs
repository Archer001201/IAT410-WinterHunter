using System.Collections;
using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class InfantrySoldierBasicAttackState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            Debug.Log("Basic Attack");
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
