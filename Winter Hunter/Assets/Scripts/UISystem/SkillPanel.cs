using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using EventHandler = EventSystem.EventHandler;

namespace UISystem
{
    public class SkillPanel : MonoBehaviour
    {
        public List<GameObject> skillIcons;
        
        private SummonSnowman _summonSnowmanScript;
        private PlayerAttribute _playerAttr;
        
        public bool isMoving;
        private const float MoveTime = 0.5f;
        private readonly List<Vector2> _targetPositions = new();
        private const float CenterThreshold = 10f;
        

        private void Awake()
        {
            var player = GameObject.FindWithTag("Player");
            _summonSnowmanScript = player.GetComponent<SummonSnowman>();
            _playerAttr = player.GetComponent<PlayerAttribute>();
        }

        private void Start()
        {
            UpdateSkillIcons();
        }

        private void OnEnable()
        {
            EventHandler.OnUpdateSkillPanel += UpdateSkillIcons;
        }

        private void OnDisable()
        {
            EventHandler.OnUpdateSkillPanel -= UpdateSkillIcons;
        }

        private void Update()
        {
            if (!isMoving)
            {
                foreach (var icon in skillIcons)
                {
                    var rectTransform = icon.GetComponent<RectTransform>();
                    UpdateIconScale(rectTransform);
                }
            }
        }

        private void UpdateSkillIcons()
        {
            var snowmanListCount = _playerAttr.snowmanList.Count;
            for (var i = 0; i < 5; i++)
            {
                skillIcons[i].GetComponent<Skill>().snowmanInfor = UpdateSnowmanBuffers(i-2, snowmanListCount);
                skillIcons[i].GetComponent<Skill>().UpdateSkillIcon();
            }
        }

        public void MoveIconsLeft()
        {
            if (isMoving) return;
            
            _targetPositions.Clear();

            foreach (var icon in skillIcons)
            {
                var rectTransform = icon.GetComponent<RectTransform>();
                var currentPos = rectTransform.anchoredPosition;
                var targetPos = new Vector2(currentPos.x - 150, currentPos.y);
                if (targetPos.x < -400) targetPos.x = 300;
                else if (targetPos.x > 400) targetPos.x = -300;
                _targetPositions.Add(targetPos);
            }

            StartCoroutine(MoveIcons());

            isMoving = true;
        }
        
        public void MoveIconsRight()
        {
            if (isMoving) return;
            
            _targetPositions.Clear();

            foreach (var icon in skillIcons)
            {
                var rectTransform = icon.GetComponent<RectTransform>();
                var currentPos = rectTransform.anchoredPosition;
                var targetPos = new Vector2(currentPos.x + 150, currentPos.y);
                if (targetPos.x < -400) targetPos.x = 300;
                else if (targetPos.x > 400) targetPos.x = -300;
                _targetPositions.Add(targetPos);
            }

            StartCoroutine(MoveIcons());
            
            isMoving = true;
        }
        
        private IEnumerator MoveIcons()
        {
            float elapsedTime = 0;

            while (elapsedTime < MoveTime)
            {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / MoveTime;

                for (var i = 0; i < skillIcons.Count; i++)
                {
                    var rectTransform = skillIcons[i].GetComponent<RectTransform>();
                    
                    rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, _targetPositions[i], t);
                
                    UpdateIconScale(rectTransform);
                }
                
                yield return null;
            }

            isMoving = false;
            UpdateSideIcons();
        }
        
        private static void SetIconScale(GameObject icon, float scale)
        {
            var rectTransform = icon.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(scale, scale, 1);
        }
        
        private static void UpdateIconScale(RectTransform rectTransform)
        {
            var distance = Mathf.Abs(rectTransform.anchoredPosition.x);
            var scale = distance <= CenterThreshold ? 1.5f : 1.0f;
            SetIconScale(rectTransform.gameObject, scale);
        }
        
        private void UpdateSideIcons()
        {
            var snowmanListCount = _playerAttr.snowmanList.Count;
            var offsetIndex = snowmanListCount switch
            {
                < 3 => 0,
                < 4 => -1,
                _ => -2
            };

            foreach (var icon in skillIcons)
            {
                var rectTransform = icon.GetComponent<RectTransform>();
                if (Mathf.Abs(rectTransform.anchoredPosition.x - 300) < CenterThreshold)
                {
                    icon.GetComponent<Skill>().snowmanInfor = UpdateSnowmanBuffers(offsetIndex, snowmanListCount);
                    icon.GetComponent<Skill>().UpdateSkillIcon();
                }
                else if (Mathf.Abs(rectTransform.anchoredPosition.x - (-300)) < CenterThreshold)
                {
                    icon.GetComponent<Skill>().snowmanInfor = UpdateSnowmanBuffers(-offsetIndex, snowmanListCount);
                    icon.GetComponent<Skill>().UpdateSkillIcon();
                }
            }
        }
        
        private SnowmanInfor UpdateSnowmanBuffers(int i, int snowmanListCount)
        {
            if (_playerAttr.snowmanList.Count < 1) return null;
            var sum = _summonSnowmanScript.currentIndex + i;
            if (_playerAttr.snowmanList.Count < 2)
            {
                sum = 0;
            }
            else
            {
                if (sum < 0)
                {
                    sum += snowmanListCount;
                }
                else if (sum > snowmanListCount - 1)
                {
                    sum -= snowmanListCount;
                }
            }
            return _playerAttr.snowmanList[sum];
        }
    }
}
