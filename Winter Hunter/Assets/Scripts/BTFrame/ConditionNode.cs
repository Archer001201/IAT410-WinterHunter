using System;

namespace BTFrame
{
    public class ConditionNode : BTNode
    {
        private readonly Func<bool> _condition;

        public ConditionNode(Func<bool> condition)
        {
            this._condition = condition;
        }
        
        public override bool Evaluate()
        {
            return _condition();
        }
    }
}