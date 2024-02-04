using BTFrame;
using UnityEngine;

namespace Snowman
{
    public class Healer : BaseSnowman
    {
        // public float healTimer;
        
        protected override void Awake()
        {
            base.Awake();
            TargetTrans = PlayerGO.transform;
        }

        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SequenceNode();
            
            var chaseNode = new SequenceNode();
            chaseNode.Children.Add(new ConditionNode(IsPlayerOutFollowRange));
            chaseNode.Children.Add(new ActionNode(SetNavigation));
            chaseNode.Children.Add(new ActionNode(StartChase));

            var idleNode = new SequenceNode();
            idleNode.Children.Add(new ConditionNode(IsPlayerInFollowRange));
            idleNode.Children.Add(new ActionNode(StopChase));
            
            rootNode.Children.Add(chaseNode);
            rootNode.Children.Add(idleNode);

            BTree = new BehaviorTree { RootNode = rootNode };
        }
        
        private bool IsPlayerOutFollowRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) > followRange;
        }

        private bool IsPlayerInFollowRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) <= followRange;
        }
    }
}
