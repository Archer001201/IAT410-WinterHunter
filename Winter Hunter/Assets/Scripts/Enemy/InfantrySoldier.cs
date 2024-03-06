using System.Collections;
using Enemy.FSM;
using UnityEngine;

namespace Enemy
{
    public class InfantrySoldier : BaseEnemy
    {
        public GameObject spearVfx;
        public bool isThrustReady = true;
        public bool isLungeReady = true;
        
        protected override void Awake()
        {
            IdleState = new NormalIdleState();
            ChaseState = new NormalChaseState();
            RetreatState = new NormalRetreatState();
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (targetTrans == null) return;
            var dist = Vector3.Distance(targetTrans.position, transform.position);
            isLungeReady = dist <= attackRange * 2 && shield > 0 && dist > attackRange;
            isThrustReady = dist <= attackRange;

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
            if (isThrustReady) StartAttacking();
            else StopAttacking();
            if (isLungeReady) StartAttacking();
            else StopAttacking();
        }

        protected override IEnumerator AttackCoroutine()
        {
            // var dist = Vector3.Distance(targetTrans.position, transform.position);
            // while (isLungeReady)
            // {
            //     // isLungeReady = false;
            //     // spearVfx.SetActive(true);
            //     var originalSpeed = Agent.speed;
            //     Agent.speed = originalSpeed * 20;
            //     // Debug.Log("lunge start");
            //     Debug.Log(Agent.speed);
            //     yield return new WaitForSeconds(1f); 
            //     
            //     Agent.speed = originalSpeed;
            //     // isLungeReady = true;
            //     // spearVfx.SetActive(false);
            //     // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
            //     // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
            // }
            while (isThrustReady)
            {
                // isThrustReady = false;
                spearVfx.SetActive(true);
                yield return new WaitForSeconds(2); 
                spearVfx.SetActive(false);
                // isThrustReady = true;
                // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
                // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
            }
            
            while (isLungeReady)
            {
                // isLungeReady = false;
                // spearVfx.SetActive(true);
                // var originalSpeed = Agent.speed;
                Agent.speed = speed * 20;
                // Debug.Log("lunge start");
                Debug.Log(Agent.speed);
                yield return new WaitForSeconds(1f); 
                
                Agent.speed = speed;
                // isLungeReady = true;
                // spearVfx.SetActive(false);
                // var createdSpearVfx = Instantiate(spearVfx, transform.position, Quaternion.identity);
                // createdSpearVfx.GetComponent<FireRing>().FollowAt(transform,attackDamage);
            }
        }
    }
}
