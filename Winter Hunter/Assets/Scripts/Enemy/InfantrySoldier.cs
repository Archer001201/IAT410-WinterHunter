using System.Collections;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class InfantrySoldier : BaseEnemy
    {
        public GameObject spearVfx;
        // public bool isThrustReady = true;
        // public bool isLungeReady = true;
        // public bool isThrustStopped = true;
        // public bool isLunging;
        
        protected override void Awake()
        {
            IdleState = new NormalIdleState();
            ChaseState = new NormalChaseState();
            RetreatState = new NormalRetreatState();

            NonAttackState = new InfantrySoldierNonAttackState();
            BasicAttackState = new InfantrySoldierBasicAttackState();
            BasicSkillState = new InfantrySoldierBasicSkillState();
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (targetTrans == null) return;
            var dist = Vector3.Distance(targetTrans.position, transform.position);
            // if ()
            // isLungeReady = dist <= attackRange * 2 && shield > 0 && dist > attackRange;
            // isThrustReady = dist <= attackRange;

            // if (!isBasicSkillSatisfied && dist <= attackRange * 2 && shield > 0 && dist > attackRange)
                isBasicSkillSatisfied = dist <= attackRange * 2 && shield > 0 && dist > attackRange;
            // if (!isBasicAttackSatisfied && dist <= attackRange)
                isBasicAttackSatisfied = dist <= attackRange;

                // if (isLunging)
                // {
                //     agent.speed = 100;
                // }
                // else
                // {
                //     agent.speed = 6;
                // }

            // if (dist <= attackRange && isThrustReady)
            // {
            //     StartAttacking();
            // }
            // else if (dist <= attackRange * 4 && shield > 0 && isLungeReady)
            // {
            //     StartAttacking();
            // }
            // if (isThrustReady || isLungeReady)
            // {
            //     StartAttacking();
            // }
            // else
            // {
            //     StopAttacking();
            // }
            // if (isThrustReady) StartAttacking();
            // else StopAttacking();
            // if (isLungeReady) StartAttacking();
            // else StopAttacking();
        }

        // protected override IEnumerator AttackCoroutine()
        // {
        //     // var dist = Vector3.Distance(targetTrans.position, transform.position);
        //     // while (isLungeReady)
        //     // {
        //     //     // isLungeReady = false;
        //     //     // spearVfx.SetActive(true);
        //     //     var originalSpeed = Agent.speed;
        //     //     Agent.speed = originalSpeed * 20;
        //     //     // Debug.Log("lunge start");
        //     //     Debug.Log(Agent.speed);
        //     //     yield return new WaitForSeconds(1f); 
        //     //     
        //     //     Agent.speed = originalSpeed;
        //     //     // isLungeReady = true;
        //     //     // spearVfx.SetActive(false);
        //     //     // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
        //     //     // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
        //     // }
        //     while (isThrustReady)
        //     {
        //         // isThrustReady = false;
        //         spearVfx.SetActive(true);
        //         yield return new WaitForSeconds(2); 
        //         spearVfx.SetActive(false);
        //         // isThrustReady = true;
        //         // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
        //         // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
        //     }
        //     
        //     while (isLungeReady)
        //     {
        //         // isLungeReady = false;
        //         // spearVfx.SetActive(true);
        //         // var originalSpeed = Agent.speed;
        //         Agent.speed = speed * 20;
        //         // Debug.Log("lunge start");
        //         Debug.Log(Agent.speed);
        //         yield return new WaitForSeconds(1f); 
        //         
        //         Agent.speed = speed;
        //         // isLungeReady = true;
        //         // spearVfx.SetActive(false);
        //         // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
        //         // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
        //     }
        // }

        public override IEnumerator BasicAttack()
        {
                var thrustVfx = spearVfx;
                if (!thrustVfx.activeSelf)
                {
                    thrustVfx.SetActive(true);
                    // Debug.Log("thrust");
                }

                yield return new WaitForSeconds(2f);
               
                // Debug.Log("switch to non attack");
                SwitchAttackingState(AttackingState.NonAttack);

        }

        public override IEnumerator BasicSkill()
        {
            agent.speed = speed * 100f;

            yield return new WaitForSeconds(1f);

            agent.speed = speed;
            
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
