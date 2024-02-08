using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class Skill : MonoBehaviour
    {
        public SnowmanInfor snowmanInfor;
        public Sprite iconSprite;
        public Image cooldownMask;
        
        private Image _skillIcon;
        

        private void Awake()
        {
            _skillIcon = GetComponent<Image>();
        }

        private void Update()
        {
            if (snowmanInfor == null) return;
            cooldownMask.fillAmount = snowmanInfor.cooldownTimer / snowmanInfor.cooldown;
        }

        public void UpdateSkillIcon()
        {
            if (snowmanInfor == null) return;
            iconSprite = Resources.Load<Sprite>("Images/" + snowmanInfor.type);
            _skillIcon.sprite = iconSprite;
        }
    }
}
