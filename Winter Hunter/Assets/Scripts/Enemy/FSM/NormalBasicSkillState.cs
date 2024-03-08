using System.Collections;

namespace Enemy.FSM
{
    public class NormalBasicSkillState : BaseState
    {
        private IEnumerator _attackCoroutine;
        
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine, CurrentEnemy.BasicSkill);
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
        }
    }
}
