using Snowman;

namespace UISystem
{
    public class SnowmanHUD : NpcHUD
    {
        public BaseSnowman snowmanAttr;

        protected override void Update()
        {
            FillPercentage1 = snowmanAttr.health / snowmanAttr.maxHealth;
            FillPercentage2 = (snowmanAttr.summoningTime - snowmanAttr.summoningTimer) / snowmanAttr.summoningTime;
            
            base.Update();
        }
    }
}
