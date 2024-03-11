using System.Collections;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class Giant : BaseEnemy
    {
        protected override void Awake()
        {
            IdleState = new NormalIdleState();
            ChaseState = new NormalChaseState();
            RetreatState = new NormalRetreatState();
            
            NonAttackState = new NormalNonAttackState();
            BasicAttackState = new NormalBasicAttackState();
            BasicSkillState = new NormalBasicSkillState();
            AdvancedSkillState = new NormalAdvancedSkillState();
            base.Awake();
        }
        
        protected override void Update()
        {
            base.Update();

            if (targetTrans == null) return;
            var dist = Vector3.Distance(targetTrans.position, transform.position);
            isBasicSkillSatisfied = dist <= attackRange * 2 && shield > 0;
            isBasicAttackSatisfied = dist <= attackRange;
            isAdvancedSkillSatisfied = dist <= attackRange;
        }

        public override IEnumerator BasicAttack()
        {
            Debug.Log("Giant Basic Attack");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator BasicSkill()
        {
            Debug.Log("Giant Basic Skill");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator AdvancedSkill()
        {
            Debug.Log("Giant Advanced Skill");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
