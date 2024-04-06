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
        public float moveSpeed = 10f; // 移动速度
        public float moveDuration = 3f;
        private Vector3 _direction;
        private Coroutine _flyCoroutine;
        public GameObject flameVfx;
        public GameObject fireBall;
        
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
            // isBasicSkillSatisfied = dist <= attackRange * 2 && shield > 0;
            // isBasicAttackSatisfied = dist >= attackRange;
            // isAdvancedSkillSatisfied = dist <= attackRange * 2;
            isBasicAttackSatisfied = dist <= attackRange * 2f;
            if (currentStage >= StageTwo) isBasicSkillSatisfied = dist <= attackRange;
            if (currentStage >= StageThree) isAdvancedSkillSatisfied = dist <= attackRange;
            
            if (shield <= 0) AfterShieldBreaking();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (targetTrans == null || animator.GetBool(EnemyAnimatorPara.IsAttacking.ToString())) return;
            var targetDirection = targetTrans.position - transform.position;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            var angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
            animator.SetFloat("Rotation", angleDifference);
        }
        
        public override IEnumerator BasicAttack()
        {
            Debug.Log("low flight");
            _direction = (targetTrans.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(_direction);
            _flyCoroutine ??= StartCoroutine(MoveTowardsTargetOverTime());

            yield return new WaitForSeconds(moveDuration);

            if (_flyCoroutine != null)
            {
                StopCoroutine(_flyCoroutine);
                _flyCoroutine = null;
            }
            
            SwitchAttackingState(AttackingState.NonAttack);
        }
        
        public override IEnumerator BasicSkill()
        {
            Debug.Log("claw attack");
            var initX = targetTrans.position.x;
            var initY = targetTrans.position.y;
            var initZ = targetTrans.position.z;
            var count = 0;
            while (count < 30)
            {
                var randX = Random.Range(initX - 10, initX + 10);
                var randY = Random.Range(initY + 20, initY + 30);
                var randZ = Random.Range(initZ - 10, initZ + 10);
                Instantiate(fireBall, new Vector3(randX, randY, randZ), Quaternion.identity);
                count++;
                yield return new WaitForSeconds(0.1f);
            }
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
        
        private IEnumerator MoveTowardsTargetOverTime() {
            var endTime = Time.time + moveDuration; // 计算结束时间
            var nextSpawnTime = Time.time + 0.5f;
            
            while (Time.time < endTime) {
                transform.position += _direction * (moveSpeed * Time.deltaTime); // 根据速度和时间移动物体
                
                if (Time.time >= nextSpawnTime) {
                    Instantiate(flameVfx, transform.position, Quaternion.identity); // 在当前位置实例化Prefab
                    nextSpawnTime += 1.2f; // 更新下一次实例化Prefab的时间
                }
                
                yield return null; // 等待下一帧
            }
        }
    }
}
