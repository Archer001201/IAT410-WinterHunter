using Utilities;

namespace Enemy.FSM
{
    public class NormalAdvancedSkillState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsAdvancedSkill.ToString(), true);
        }

        public override void OnUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnExist()
        {
            // throw new System.NotImplementedException();
            CurrentEnemy.StopCurrentCoroutine(CurrentEnemy.AdvancedSkillCoroutine);
            CurrentEnemy.advancedSkillTimer = CurrentEnemy.advancedSkillCooldown;
            
            if (CurrentEnemy.animator == null) return; 
            CurrentEnemy.animator.SetBool(EnemyAnimatorPara.IsAdvancedSkill.ToString(), false);
        }

        public override void OnCall()
        {
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.AdvancedSkillCoroutine, CurrentEnemy.AdvancedSkill);
        }
    }
}
