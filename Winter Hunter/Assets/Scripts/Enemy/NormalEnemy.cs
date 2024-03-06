using System.Collections;
using System.Reflection;
using BTFrame;
using Enemy.FSM;
using UnityEngine;

namespace Enemy
{
    /*
     * Normal enemy extends from base enemy
     */
    public class NormalEnemy : BaseEnemy
    {
        [Header("Normal Enemy Settings")]
        public GameObject fireRing;

        protected override void Awake()
        {
            IdleState = new NormalIdleState();
            ChaseState = new NormalChaseState();
            RetreatState = new NormalRetreatState();
            base.Awake();
        }

        /*
         * Update target game object and reset destination for NavMesh Agent
         */
        // public override void SetTarget(Transform tar)
        // {
        //     base.SetTarget(tar);
        //     // MoveTowardsTarget();
        // }
        
        /*
         * Every 2 seconds create a fire ring if in attacking state
         */
        protected override IEnumerator AttackCoroutine()
        {
            if (targetTrans == null)
            {
                Debug.Log("null");
                yield return null;
            }
            while (Vector3.Distance(targetTrans.position, transform.position) <= attackRange)
            {
                // if (targetTrans == null)
                // {
                //     Debug.Log("target null");
                //     yield return null;
                // }
                
                yield return new WaitForSeconds(2); 
                var createdFireRing = Instantiate(fireRing, transform.position, Quaternion.identity);
                createdFireRing.GetComponent<FireRing>().FollowAt(transform,attackDamage);
            }
        }
    }
}