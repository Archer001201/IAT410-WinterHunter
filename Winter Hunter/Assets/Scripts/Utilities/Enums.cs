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
        NonAttack, BasicAttack, BasicSkill, AdvancedSkill
    }

    public enum FovType
    {
        Narrative, Normal, Battle
    }

    public enum GameDataSet
    {
        Data1, Data2, Data3
    }

    public enum PlayerAnimationPara
    {
        Angle, IsThrowing, IsRolling, IsDead
    }

    public enum EnemyAnimatorPara
    {
        IsMoving, IsBasicAttack, IsBasicSkill, IsAttacking, IsAdvancedSkill, IsDead, Possibility
    }
}
