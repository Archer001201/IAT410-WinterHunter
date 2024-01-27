using BTFrame;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Enemy
{
    public class NormalEnemy : BasicEnemy
    {
        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SelectorNode();

            var idleNode = new SequenceNode();
            idleNode.Children.Add(new ConditionNode(IsBeyondDistance));
            idleNode.Children.Add(new ActionNode(Idle));
            
            var chaseNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsPlayerInChaseRange));
            chaseNode.Children.Add(new ActionNode(Chase));
            
            rootNode.Children.Add(idleNode);
            rootNode.Children.Add(chaseNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }
        
        private static void Idle()
        {
            Debug.Log("idle");
        }

        private void Chase()
        {
            if (Agent.isActiveAndEnabled)
            {
                Agent.SetDestination(PlayerTrans.position);
            }
        }

        private bool IsPlayerInChaseRange()
        {
            return Vector3.Distance(transform.position, PlayerTrans.position) < 20f;
        }

        private static bool IsBeyondDistance()
        {
            return Input.GetKeyDown("z");
        }
    }
}