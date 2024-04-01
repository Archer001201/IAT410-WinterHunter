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
        
        protected override void Awake()
        {
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (targetTrans == null) return;
            var targetDirection = targetTrans.position - transform.position;
            // 创建代表这个方向的旋转
            var targetRotation = Quaternion.LookRotation(targetDirection);
            // 使用Slerp平滑地插值从当前旋转到目标旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        public override IEnumerator BasicAttack()
        {
            Debug.Log("low flight");
            yield return new WaitForSeconds(1f);
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
            yield return new WaitForSeconds(1f);
            SwitchAttackingState(AttackingState.NonAttack);
        }
    }
}
