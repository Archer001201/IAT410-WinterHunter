using System.Collections;
using System.Collections.Generic;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class Giant : BaseEnemy
    {
        public GameObject smashVfx;
        public List<Transform> smashTransList;
        public GameObject swingVfx;
        public GameObject rock;
        public Transform throwPoint; // 扔石头的起始位置
        public float throwAngle = 45.0f; // 扔出的角度，45度可以获得最远的距离
        public float gravity = -Physics.gravity.y; // 使用重力加速度
        
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
            isBasicSkillSatisfied = dist <= attackRange && shield > 0;
            isBasicAttackSatisfied = dist <= attackRange;
            isAdvancedSkillSatisfied = dist <= attackRange * 2;
        }

        public override IEnumerator BasicAttack()
        {
            // Debug.Log("Giant Basic Attack");
            // agent.isStopped = true;
            agent.speed = 0f;
            
            yield return new WaitForSeconds(0.3f);
            // var i = 0;
            // while (i < smashTransList.Count)
            // {
            //     var shockwave = Instantiate(smashVfx, smashTransList[i].position, Quaternion.identity);
            //     shockwave.GetComponent<FireRing>().SetFireRing(attackDamage);
            //     i++;
            //     yield return new WaitForSeconds(0.2f);
            // }
            var smash = Instantiate(smashVfx, transform.position, Quaternion.identity);
            smash.GetComponent<Smash>().SetAttack(attackDamage);
            agent.speed = speed;
            // agent.isStopped = false;
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator BasicSkill()
        {
            // Debug.Log("Giant Basic Skill");
            swingVfx.SetActive(true);
            yield return new WaitForSeconds(5f);
            swingVfx.SetActive(false);
            SwitchAttackingState(AttackingState.NonAttack);
        }

        public override IEnumerator AdvancedSkill()
        {
            // Debug.Log("Giant Advanced Skill");
            var rockGO = Instantiate(rock, throwPoint.position, Quaternion.identity);
            rockGO.GetComponent<ThrowingRock>().SetAttack(attackDamage);
            LaunchStone(rockGO);
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }
        
        private void LaunchStone(GameObject stone)
        {
            var rb = stone.GetComponent<Rigidbody>();

            var targetDir = targetTrans.position - throwPoint.position; // 获取朝向玩家的方向
            var distance = targetDir.magnitude; // 获取到玩家的距离
            var throwAngleRad = throwAngle * Mathf.Deg2Rad; // 将扔出角度转换为弧度

            // 计算初始速度
            var velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * throwAngleRad));

            var velocityXZ = targetDir.normalized * velocity * Mathf.Cos(throwAngleRad);
            var velocityY = velocity * Mathf.Sin(throwAngleRad);

            var finalVelocity = new Vector3(velocityXZ.x, velocityY, velocityXZ.z);

            rb.velocity = finalVelocity; // 应用初始速度到石头
        }
    }
}
