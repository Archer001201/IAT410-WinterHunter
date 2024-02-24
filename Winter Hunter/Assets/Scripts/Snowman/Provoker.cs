using BTFrame;
using EventSystem;
using UnityEngine;

namespace Snowman
{
    /*
     * A type of snowman can taunt enemy
     */
    public class Provoker : BaseSnowman
    {
        protected override void Awake()
        {
            base.Awake();
            // TargetTrans = PlayerGO.transform;
            EventHandler.EnemyChangeTarget(gameObject);
        }

        protected override void Update()
        {
            base.Update();
            if (health > 0 && summonTimer < MySnowmanSO.summonDuration) return;
            // EventHandler.EnemyChangeTarget(PlayerGO);
        }

        /*
         * Setup and initialize the behaviour tree
         */
        // protected override void SetUpBehaviorTree()
        // {
        //     base.SetUpBehaviorTree();
        //     var rootNode = new SelectorNode();
        //
        //     var chaseNode = new SequenceNode();
        //     chaseNode.Children.Add(new ConditionNode(IsPlayerOutFollowRange));
        //     chaseNode.Children.Add(new ActionNode(SetNavigation));
        //     chaseNode.Children.Add(new ActionNode(StartChase));
        //
        //     var idleNode = new SequenceNode();
        //     idleNode.Children.Add(new ConditionNode(IsPlayerInFollowRange));
        //     idleNode.Children.Add(new ActionNode(StopChase));
        //     
        //     rootNode.Children.Add(chaseNode);
        //     rootNode.Children.Add(idleNode);
        //
        //     BTree = new BehaviorTree { RootNode = rootNode };
        // }

        /*
         * Check is the player out of following range
         */
        // private bool IsPlayerOutFollowRange()
        // {
        //     return Vector3.Distance(transform.position, TargetTrans.position) > followRange;
        // }
        //
        // /*
        //  * Check is the player in the following range
        //  */
        // private bool IsPlayerInFollowRange()
        // {
        //     return Vector3.Distance(transform.position, TargetTrans.position) <= followRange;
        // }

        /*
         * Destroy this game object and notice enemy to change target to player
         */
        protected override void DestroyMe()
        {
            // EventHandler.EnemyChangeTarget(PlayerGO);
            base.DestroyMe();
        }
    }
}
