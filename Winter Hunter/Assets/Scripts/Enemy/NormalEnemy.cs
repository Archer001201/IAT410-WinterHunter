using System.Collections;
using BTFrame;
using UnityEngine;

namespace Enemy
{
    public class NormalEnemy : BaseEnemy
    {
        [Header("Normal Enemy Settings")]
        public GameObject fireRing;
        public float chaseAndAttackRange;
        
        private Coroutine _attackCoroutine;

        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SelectorNode();
            
            var chaseNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsTargetInChaseRange));
            chaseNode.Children.Add(new ActionNode(StopAttack));
            chaseNode.Children.Add(new ActionNode(SetNavigation));
            chaseNode.Children.Add(new ActionNode(StartChase));
            
            var attackNode = new SequenceNode();
            attackNode.Children.Add(new ConditionNode(IsTargetInAttackRange));
            attackNode.Children.Add(new ActionNode(StartAttack));
            attackNode.Children.Add(new ActionNode(StopChase));
            
            var chaseAndAttackNode = new SequenceNode();
            chaseAndAttackNode.Children.Add(new ConditionNode(IsTargetInChaseAndAttackRange));
            chaseAndAttackNode.Children.Add(new ActionNode(StartAttack));
            chaseAndAttackNode.Children.Add(new ActionNode(SetNavigation));
            chaseAndAttackNode.Children.Add(new ActionNode(StartChase));
            
            rootNode.Children.Add(chaseNode);
            rootNode.Children.Add(attackNode);
            rootNode.Children.Add(chaseAndAttackNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }

        private void SetNavigation()
        {
            if (Agent.isActiveAndEnabled)
            {
                Agent.SetDestination(TargetTrans.position);
            }
        }

        private void StartChase()
        {
            if (Agent.isStopped) Agent.isStopped = false;
        }

        private void StopChase()
        {
            if (!Agent.isStopped) Agent.isStopped = true;
        }

        private void StartAttack()
        {
            _attackCoroutine ??= StartCoroutine(AttackCoroutine());
        }

        private void StopAttack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
        
        private IEnumerator AttackCoroutine()
        {
            while (IsTargetInAttackRange() || IsTargetInChaseAndAttackRange())
            {
                var createdFireRing = Instantiate(fireRing, transform.position, Quaternion.identity);
                createdFireRing.GetComponent<FireRing>().FollowAt(transform,attackDamage);
                yield return new WaitForSeconds(2); 
            }
        }
        
        private bool IsTargetInChaseRange()
        {
            var distance = Vector3.Distance(transform.position, TargetTrans.position);
            return distance < chaseRange && distance > chaseAndAttackRange;
        }
        
        private bool IsTargetInAttackRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) <= attackRange;
        }
        
        private bool IsTargetInChaseAndAttackRange()
        {
            var distance = Vector3.Distance(transform.position, TargetTrans.position);
            return distance <= chaseAndAttackRange && distance > attackRange;
        }
    }
}