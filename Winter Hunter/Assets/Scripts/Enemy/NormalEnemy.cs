using BTFrame;
using UnityEngine;

namespace Enemy
{
    public class NormalEnemy : BasicEnemy
    {
        [Header("Normal Enemy Settings")] public ParticleSystem fireRing;
        
        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SelectorNode();
            
            var chaseNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsTargetInChaseRange));
            chaseNode.Children.Add(new ActionNode(Chase));
            
            var attackNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsTargetInAttackRange));
            chaseNode.Children.Add(new ActionNode(Attack));
            
            rootNode.Children.Add(chaseNode);
            rootNode.Children.Add(attackNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }

        private void Chase()
        {
            if (Agent.isActiveAndEnabled)
            {
                Agent.SetDestination(TargetTrans.position);
            }
            
            if (fireRing.isPlaying) fireRing.Stop();
        }

        private void Attack()
        {
            if (fireRing.isStopped) fireRing.Play();
        }
        
        private bool IsTargetInChaseRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) < chaseRange;
        }
        
        private bool IsTargetInAttackRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) < attackRange;
        }
    }
}