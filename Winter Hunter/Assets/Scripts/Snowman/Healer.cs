using BTFrame;
using UnityEngine;

namespace Snowman
{
    /*
     * A type of snowman that can heal player by the healing ring
     */
    public class Healer : BaseSnowman
    {
        protected override void Awake()
        {
            base.Awake();
            TargetTrans = PlayerGO.transform;
        }

        /*
         * Set up and initialize the behaviour tree
         */
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
        
        /*
         * Check is the player out the following range
         */
        private bool IsPlayerOutFollowRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) > followRange;
        }

        /*
         * Check is the player in the following range
         */
        private bool IsPlayerInFollowRange()
        {
            return Vector3.Distance(transform.position, TargetTrans.position) <= followRange;
        }
    }
}
