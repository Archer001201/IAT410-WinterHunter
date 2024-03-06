using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class InfantrySoldierNonAttackState : BaseState
    {
        public override void OnEnter(BaseEnemy enemy)
        {
            // throw new System.NotImplementedException();
            // if (enemy is InfantrySoldier soldier)
            // {
            //     CurrentEnemy = soldier;
            // }
            Debug.Log("Non Attack");
            CurrentEnemy = enemy;
        }

        public override void OnUpdate()
        {
            // throw new System.NotImplementedException();
            // if (CurrentEnemy is InfantrySoldier infantrySoldier)
            // {
            //     if (infantrySoldier.isLungeReady) infantrySoldier.SwitchAttackingState(AttackingState.BasicSkill);
            //     else if (infantrySoldier.isThrustReady) infantrySoldier.SwitchAttackingState(AttackingState.BasicAttack);
            // }
            if (CurrentEnemy.isBasicAttackReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicAttack);
            else if (CurrentEnemy.isBasicSkillReady) CurrentEnemy.SwitchAttackingState(AttackingState.BasicSkill);
        }

        public override void OnFixedUpdate()
        {
            // throw new System.NotImplementedException();
            // if (CurrentEnemy.basicAttackCooldown > 0) CurrentEnemy.basicAttackCooldown -= Time.fixedDeltaTime;
            // if (CurrentEnemy.basicSkillCooldown > 0) CurrentEnemy.basicSkillCooldown -= Time.fixedDeltaTime;
        }

        public override void OnExist()
        {
            // throw new System.NotImplementedException();
        }
    }
}
