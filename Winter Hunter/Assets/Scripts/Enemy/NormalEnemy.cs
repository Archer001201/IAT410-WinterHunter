using System.Collections;
using BTFrame;
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

        /*
         * Update target game object and reset destination for NavMesh Agent
         */
        public override void SetTarget(GameObject tar)
        {
            base.SetTarget(tar);
            MoveTowardsTarget();
        }
        
        /*
         * Every 2 seconds create a fire ring if in attacking state
         */
        protected override IEnumerator AttackCoroutine()
        {
            while (Vector3.Distance(TargetTrans.position, transform.position) <= attackRange)
            {
                yield return new WaitForSeconds(2); 
                var createdFireRing = Instantiate(fireRing, transform.position, Quaternion.identity);
                createdFireRing.GetComponent<FireRing>().FollowAt(transform,attackDamage);
            }
        }
    }
}