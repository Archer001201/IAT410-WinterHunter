namespace Enemy.FSM
{
    public class NormalAdvancedSkillState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            CurrentEnemy = enemy;
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.AdvancedSkillCoroutine, CurrentEnemy.AdvancedSkill);
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
        }
    }
}
