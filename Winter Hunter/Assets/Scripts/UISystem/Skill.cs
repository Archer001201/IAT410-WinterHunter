using DataSO;
using Player;
using Snowman;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    /*
     * Control skill icon
     */
    public class Skill : MonoBehaviour
    {
        public SnowmanInfo snowmanInfo;
        public Sprite iconSprite;
        public Image cooldownMask;
        
        private Image _skillIcon;
        

        private void Awake()
        {
            _skillIcon = GetComponent<Image>();
        }

        private void Update()
        {
            if (snowmanInfo == null) return;
            cooldownMask.fillAmount = snowmanInfo.cooldownTimer / snowmanInfo.cooldown;
        }

        /*
         * Update skill icon on skill panel
         */
        public void UpdateSkillIcon()
        {
            if (snowmanInfo == null) return;
            // iconSprite = Resources.Load<Sprite>("Images/" + snowmanInfor.type);
            iconSprite = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + snowmanInfo.type + "_SO").snowmanIcon;
            _skillIcon.sprite = iconSprite;
        }
    }
}
