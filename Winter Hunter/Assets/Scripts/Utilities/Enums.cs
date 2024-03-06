namespace Utilities
{

    public enum SnowmanType
    {
        Warrior, Guardian, Healer, Provoker, Marksman, Alchemist 
    }

    public enum SnowmanLevel
    {
        Basic, Advanced
    }

    public enum MovementMode
    {
        ChaseEnemy, FollowPlayer, Stationary
    }

    public enum ShieldBreakEfficiency
    {
        Low, Median, High
    }

    public enum MovingState
    {
        Chase, Retreat, Idle
    }

    public enum AttackingState
    {
        NonAttack, BasicAttack, BasicSkill
    }
}
