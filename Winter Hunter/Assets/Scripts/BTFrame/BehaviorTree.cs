namespace BTFrame
{
    public class BehaviorTree
    {
        public BTNode RootNode;

        public void BTUpdate()
        {
            RootNode.Evaluate();
        }
    }
}