using System.Collections;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class InfantrySoldier : BaseEnemy
    {
        public GameObject spearVfx;
        public GameObject trailVfx;
        
        protected override void Awake()
        {
            IdleState = new NormalIdleState();
            ChaseState = new NormalChaseState();
            RetreatState = new NormalRetreatState();

            NonAttackState = new NormalNonAttackState();
            BasicAttackState = new NormalBasicAttackState();
            BasicSkillState = new NormalBasicSkillState();
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (targetTrans == null) return;
            var dist = Vector3.Distance(targetTrans.position, transform.position);
            isBasicSkillSatisfied = dist <= attackRange * 2 && shield > 0 && dist > attackRange;
            isBasicAttackSatisfied = dist <= attackRange;
        }

        public override IEnumerator BasicAttack()
        {
            var thrustVfx = spearVfx; 
            if (!thrustVfx.activeSelf)
            { 
                thrustVfx.SetActive(true);
            }
            
            yield return new WaitForSeconds(2f);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator BasicSkill()
        {
            agent.speed = speed * 50f;
            trailVfx.SetActive(true);

            yield return new WaitForSeconds(2f);

            agent.speed = speed;
            trailVfx.SetActive(false);
            
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
