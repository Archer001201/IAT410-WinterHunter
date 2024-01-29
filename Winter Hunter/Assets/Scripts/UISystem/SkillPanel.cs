using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SkillPanel : MonoBehaviour
    {
        public GameObject skillIconPrefab;
        public GameObject skillContainer;
        
        private SummonSnowman _summonSnowmanScript;

        private void Awake()
        {
            _summonSnowmanScript = GameObject.FindWithTag("Player").GetComponent<SummonSnowman>();
            if (_summonSnowmanScript == null) return;
            foreach (var item in _summonSnowmanScript.snowmanList)
            {
                var icon = Resources.Load<Sprite>("Images/" + item);
                if (icon != null) Debug.Log("icon");
                var skillIcon = Instantiate(skillIconPrefab, skillContainer.transform);
                skillIcon.GetComponent<Image>().sprite = icon;
            }
        }
    }
}
