using BTFrame;
using EventSystem;
using UnityEngine;

namespace Snowman
{
    public class MeatShield : BaseSnowman
    {
        protected override void Awake()
        {
            base.Awake();
            TargetTrans = PlayerGO.transform;
            EventHandler.EnemyChangeTarget(gameObject);
        }

        protected override void Update()
        {
            base.Update();
            if (health > 0 && summoningTimer < snowmanSO.summoningTime) return;
            EventHandler.EnemyChangeTarget(PlayerGO);
        }

        protected override void SetUpBehaviorTree()
        {
            base.SetUpBehaviorTree();
            var rootNode = new SelectorNode();

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

        protected override void DestroyMe()
        {
            EventHandler.EnemyChangeTarget(PlayerGO);
            base.DestroyMe();
        }
    }
}
