using System;

namespace BTFrame
{
    public class ActionNode : BTNode
    {
        private readonly Action _action;

        public ActionNode(Action action)
        {
            this._action = action;
        }
        
        public override bool Evaluate()
        {
            _action();
            return true;
        }
    }
}