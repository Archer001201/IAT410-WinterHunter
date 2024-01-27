using System;
using System.Collections.Generic;

namespace BTFrame
{
    public class SelectorNode : BTNode
    {
        public readonly List<BTNode> Children = new();
        
        public override bool Evaluate()
        {
            foreach (var child in Children)
            {
                if (child.Evaluate())
                {
                    return true;
                }
            }

            return false;
        }
    }
}