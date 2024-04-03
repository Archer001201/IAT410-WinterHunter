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
            isBasicSkillSatisfied = dist <= attackRange * 2 && shield > 0;
            isBasicAttackSatisfied = dist >= attackRange;
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
            // _rb.AddForce(direction * (_rb.mass * 100000f));
            // _rb.AddForce(transform.forward,ForceMode.Impulse);
            // transform.Translate(targetTrans.position);
            // agent.speed = 20f;
            _direction = (targetTrans.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(_direction);
            _flyCoroutine ??= StartCoroutine(MoveTowardsTargetOverTime());
            // while (_flyCoroutine != null)
            // {
            //     Instantiate(flameVfx, transform.position, Quaternion.identity);
            //     yield return new WaitForSeconds(1f);
            // }
            yield return new WaitForSeconds(moveDuration);
            // agent.speed = 0f;
            // agent.velocity = Vector3.zero;
            // agent.isStopped = true;
            // yield return new WaitForSeconds(3f); 
            // _rb.velocity = Vector3.zero;
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
                var randX = Random.Range(initX - 15, initX + 15);
                var randY = Random.Range(initY + 20, initY + 30);
                var randZ = Random.Range(initZ - 15, initZ + 15);
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
