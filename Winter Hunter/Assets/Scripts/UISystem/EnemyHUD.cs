using Enemy;

namespace UISystem
{
    public class EnemyHUD : NpcHUD
    {
        public BaseEnemy enemyAttr;

        protected override void Update()
        {
            FillPercentage1 = enemyAttr.health / enemyAttr.maxHealth;
            FillPercentage2 = enemyAttr.shield / enemyAttr.maxShield;
            
            base.Update();
        }
    }
}