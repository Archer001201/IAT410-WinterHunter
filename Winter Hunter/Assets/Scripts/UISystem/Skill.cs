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
            cooldownMask.fillAmount = snowmanInfor.cooldownTimer / snowmanInfor.cooldown;
        }

        public void UpdateSkillIcon()
        {
            iconSprite = Resources.Load<Sprite>("Images/" + snowmanInfor.type);
            _skillIcon.sprite = iconSprite;
        }
    }
}
