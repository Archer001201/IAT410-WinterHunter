using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class InfantrySoldierNonAttackState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
        }

        public override void OnUpdate()
        {
            if (CurrentEnemy.isBasicAttackReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicAttack);
            else if (CurrentEnemy.isBasicSkillReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicSkill);
        }

        public override void OnFixedUpdate()
        {
            
        }

        public override void OnExist()
        {

        }
    }
}
