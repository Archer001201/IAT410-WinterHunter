using System.Collections.Generic;

namespace BTFrame
{
    public class SequenceNode : BTNode
    {
        public readonly List<BTNode> Children = new();
        
        public override bool Evaluate()
        {
            foreach (var child in Children)
            {
                if (!child.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }
    }
}