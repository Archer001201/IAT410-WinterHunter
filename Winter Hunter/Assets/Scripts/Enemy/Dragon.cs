using System.Collections;
using DG.Tweening;
using Enemy.FSM;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class Dragon : BaseEnemy
    {
        public float rotationSpeed = 1f;
        public GameObject arcFireVfx;
        private Rigidbody _rb;
        
        protected override void Awake()
        {
            NonAttackState = new NormalNonAttackState();
            BasicAttackState = new NormalBasicAttackState();
            BasicSkillState = new NormalBasicSkillState();
            AdvancedSkillState = new NormalAdvancedSkillState();
            _rb = GetComponent<Rigidbody>();
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (targetTrans == null || animator.GetBool(EnemyAnimatorPara.IsAttacking.ToString())) return;
            var targetDirection = targetTrans.position - transform.position;
            // 创建代表这个方向的旋转
            var targetRotation = Quaternion.LookRotation(targetDirection);
            // 使用Slerp平滑地插值从当前旋转到目标旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            var angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
            animator.SetFloat("Rotation", angleDifference);
        }
        
        public override IEnumerator BasicAttack()
        {
            Debug.Log("low flight");
            // _rb.AddForce(transform.forward,ForceMode.Impulse);
            // transform.Translate(targetTrans.position);
            agent.speed = 20f;
            yield return new WaitForSeconds(3f);
            agent.speed = 0f;
            agent.isStopped = true;
            yield return new WaitForSeconds(3f); 
            SwitchAttackingState(AttackingState.NonAttack);
        }
        
        public override IEnumerator BasicSkill()
        {
            Debug.Log("claw attack");
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }
        
        public override IEnumerator AdvancedSkill()
        {
            Debug.Log("fire attack");
            arcFireVfx.SetActive(true);
            yield return new WaitForSeconds(5f);
            arcFireVfx.SetActive(false);
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
