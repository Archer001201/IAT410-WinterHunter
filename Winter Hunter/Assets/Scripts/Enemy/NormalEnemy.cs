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
            
            rootNode.Children.Add(idleNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }

        private static bool IsBeyondDistance()
        {
            return Input.GetKeyDown("z");
        }

        private void Idle()
        {
            Debug.Log("idle");
        }
    }
}