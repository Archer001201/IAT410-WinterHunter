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
        public float chaseAndAttackRange;

        /*
         * Set up and initialize the behaviour tree
         */
        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SelectorNode();

            var idleNode = new SequenceNode();
            idleNode.Children.Add(new ConditionNode(IsInCampRange));
            idleNode.Children.Add(new ConditionNode(IsOutOfChaseRange));
            idleNode.Children.Add(new ActionNode(StopChase));

            var goBackNode = new SequenceNode();
            goBackNode.Children.Add(new ConditionNode(IsOutOfCampRange));
            goBackNode.Children.Add(new ConditionNode(IsOutOfChaseRange));
            goBackNode.Children.Add(new ActionNode(GoBackToCamp));
            goBackNode.Children.Add(new ActionNode(StopAttack));
            goBackNode.Children.Add(new ActionNode(StartChase));
            
            var chaseNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsTargetInChaseRange));
            chaseNode.Children.Add(new ActionNode(StopAttack));
            chaseNode.Children.Add(new ActionNode(SetNavigation));
            
            var attackNode = new SequenceNode();
            attackNode.Children.Add(new ConditionNode(IsTargetInAttackRange));
            attackNode.Children.Add(new ActionNode(StartAttack));
            attackNode.Children.Add(new ActionNode(StopChase));
            
            var chaseAndAttackNode = new SequenceNode();
            chaseAndAttackNode.Children.Add(new ConditionNode(IsTargetInChaseAndAttackRange));
            chaseAndAttackNode.Children.Add(new ActionNode(StartAttack));
            chaseAndAttackNode.Children.Add(new ActionNode(SetNavigation));
            chaseAndAttackNode.Children.Add(new ActionNode(StartChase));
            
            rootNode.Children.Add(idleNode);
            rootNode.Children.Add(goBackNode);
            rootNode.Children.Add(chaseNode);
            rootNode.Children.Add(attackNode);
            rootNode.Children.Add(chaseAndAttackNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }

        /*
         * Update target game object and reset destination for NavMesh Agent
         */
        protected override void UpdateTarget(GameObject tar)
        {
            base.UpdateTarget(tar);
            SetNavigation();
        }
        
        /*
         * Every 2 seconds create a fire ring if in attacking state
         */
        protected override IEnumerator AttackCoroutine()
        {
            while (IsTargetInAttackRange() || IsTargetInChaseAndAttackRange())
            {
                var createdFireRing = Instantiate(fireRing, transform.position, Quaternion.identity);
                createdFireRing.GetComponent<FireRing>().FollowAt(transform,attackDamage);
                yield return new WaitForSeconds(2); 
            }
        }
        
        /*
         * Check is target in chase range
         */
        private bool IsTargetInChaseRange()
        {
            var distance = Vector3.Distance(transform.position, TargetTrans.position);
            return distance < chaseRange && distance > chaseAndAttackRange;
        }
        
        /*
         * Check is target in attack range
         */
        private bool IsTargetInAttackRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) <= attackRange;
        }
        
        /*
         * Check is target in the chase and attack range
         */
        private bool IsTargetInChaseAndAttackRange()
        {
            var distance = Vector3.Distance(transform.position, TargetTrans.position);
            return distance <= chaseAndAttackRange && distance > attackRange;
        }
    }
}