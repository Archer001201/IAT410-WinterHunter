using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class NormalNonAttackState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsAttacking.ToString(), false);
        }

        public override void OnUpdate()
        {
            if (CurrentEnemy == null)
            {
                Debug.Log("null");
                return;
            }
            if (CurrentEnemy.isAdvancedSkillReady) CurrentEnemy.SwitchAttackingState(AttackingState.AdvancedSkill);
            else if (CurrentEnemy.isBasicAttackReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicAttack);
            else if (CurrentEnemy.isBasicSkillReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicSkill);
        }

        public override void OnFixedUpdate()
        {
            
        }

        public override void OnExist()
        {
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsAttacking.ToString(), true);
        }
    }
}
