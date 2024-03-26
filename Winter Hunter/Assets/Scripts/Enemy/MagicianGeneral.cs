using System.Collections;
using System.Collections.Generic;
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
        public Transform throwPoint;
        public GameObject smashVfx;
        public List<Transform> smashTransList;
        
        protected override void Awake()
        {
            // IdleState = new NormalIdleState();
            // ChaseState = new NormalChaseState();
            // RetreatState = new NormalRetreatState();
            
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
            var fireball = Instantiate(fireBallLarge, throwPoint.position, Quaternion.identity);
            fireball.GetComponent<FireBall>().SetFireBall(transform.forward, attackDamage);
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator BasicSkill()
        {
            agent.speed = 0f;
            
            yield return new WaitForSeconds(1f);
            var i = 0;
            while (i < smashTransList.Count)
            {
                var shockwave = Instantiate(smashVfx, smashTransList[i].position, Quaternion.identity);
                shockwave.GetComponent<FireRing>().SetFireRing(attackDamage);
                i++;
                yield return new WaitForSeconds(0.2f);
            }
            // var smash = Instantiate(smashVfx, transform.position, Quaternion.identity);
            // smash.GetComponent<Smash>().SetAttack(attackDamage);
            agent.speed = speed;
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator AdvancedSkill()
        {
            // Debug.Log("Giant Basic Skill");
            var raysGO = Instantiate(flameRays, transform.position, Quaternion.identity);
            var raysScript = raysGO.GetComponent<FlameRays>();
            raysScript.SetFlameRays(transform, attackDamage);
            yield return new WaitForSeconds(10f);
            // flameRays.SetActive(false);
            raysScript.DestroyMe();
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
