using System;
using BTFrame;
using UnityEngine;

namespace Snowman
{
    public class MeatShield : BaseSnowman
    {
        [Header("Meat shield Settings")] 
        public float followRange;
        
        private Transform _playerTrans;

        private void Start()
        {
            _playerTrans = PlayerGO.transform;
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
        
        private void SetNavigation()
        {
            if (Agent.isActiveAndEnabled && _playerTrans != null)
            {
                Agent.SetDestination(_playerTrans.position);
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

        private bool IsPlayerOutFollowRange()
        {
            return Vector3.Distance(transform.position, _playerTrans.position) > followRange;
        }

        private bool IsPlayerInFollowRange()
        {
            return Vector3.Distance(transform.position, _playerTrans.position) <= followRange;
        }
    }
}
