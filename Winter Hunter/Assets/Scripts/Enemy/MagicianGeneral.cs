using System.Collections;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class MagicianGeneral : BaseEnemy
    {
        public GameObject flameRays;
        public GameObject fireBallLarge;
        public GameObject fireBallSmall;
        
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
            isAdvancedSkillSatisfied = dist <= attackRange * 2;
        }

        public override IEnumerator BasicAttack()
        {
            Debug.Log("basic attack");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator BasicSkill()
        {
            // Debug.Log("Giant Basic Skill");
            var raysGO = Instantiate(flameRays, transform.position, Quaternion.identity);
            var raysScript = raysGO.GetComponent<FlameRays>();
            raysScript.Follow(transform);
            yield return new WaitForSeconds(10f);
            // flameRays.SetActive(false);
            raysScript.DestroyMe();
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator AdvancedSkill()
        {
            Debug.Log("advanced skill");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
