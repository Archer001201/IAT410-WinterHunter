using System.Collections;
using Utilities;

namespace Enemy.FSM
{
    public class NormalBasicSkillState : BaseState
    {
        private IEnumerator _attackCoroutine;
        
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            // CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine, CurrentEnemy.BasicSkill);
            
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsBasicSkill.ToString(), true);
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExist()
        {
            CurrentEnemy.StopCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine);
            CurrentEnemy.basicSkillTimer = CurrentEnemy.basicSkillCooldown;
            
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsBasicSkill.ToString(), false);
        }

        public override void OnCall()
        {
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine, CurrentEnemy.BasicSkill);
        }
    }
}
