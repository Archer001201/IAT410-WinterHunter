using Enemy;
using UnityEngine;

namespace UISystem
{
    /*
     * Control enemy's HUD
     */
    public class EnemyHUD : NpcHUD
    {
        public BaseEnemy enemyAttr;
        public GameObject targetSign;

        protected override void Update()
        {
            FillPercentage1 = enemyAttr.health / enemyAttr.maxHealth;
            FillPercentage2 = enemyAttr.shield / enemyAttr.maxShield;
            
            base.Update();
        }

        public void SetTargetSign(bool isTargeted)
        {
            targetSign.SetActive(isTargeted);
        }
    }
}
