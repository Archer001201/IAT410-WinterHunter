using DataSO;
using Snowman;

namespace UISystem
{
    public class SnowmanHUD : NpcHUD
    {
        /*
         * Snowman's HUD
         */
        public BaseSnowman snowmanAttr;
        public SnowmanSO snowmanSO;

        protected override void Update()
        {
            FillPercentage1 = snowmanAttr.health / snowmanSO.maxHealth;
            FillPercentage2 = (snowmanSO.summoningTime - snowmanAttr.summoningTimer) / snowmanSO.summoningTime;
            
            base.Update();
        }
    }
}
