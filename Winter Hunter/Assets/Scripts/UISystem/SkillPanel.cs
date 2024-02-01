using System.Collections;
using System.Collections.Generic;
using DataSO;
using Player;
using Snowman;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SkillPanel : MonoBehaviour
    {
        public List<GameObject> skillIcons;
        
        private SummonSnowman _summonSnowmanScript;
        private int _currentIndex;
        private PlayerSO _playerSO;
        
        public bool isMoving;
        private const float MoveTime = 0.5f;
        private readonly List<Vector2> _targetPositions = new();
        private const float CenterThreshold = 10f;
        

        private void Awake()
        {
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            _summonSnowmanScript = GameObject.FindWithTag("Player").GetComponent<SummonSnowman>();
            if (_summonSnowmanScript == null) return;
            _currentIndex = _summonSnowmanScript.currentIndex;
            var snowmanListCount = _playerSO.snowmanList.Count;

            for (var i = -2; i < 3; i++)
            {
                var index = i + 2;
                skillIcons[index].GetComponent<Image>().sprite = GetIconSprite(i, snowmanListCount);
            }
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

        public void MoveIconsLeft()
        {
            if (isMoving) return;
            isMoving = true;
            _targetPositions.Clear();
            _currentIndex = _summonSnowmanScript.currentIndex;

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

            UpdateIconImage();
        }
        
        public void MoveIconsRight()
        {
            if (isMoving) return;
            isMoving = true;
            _targetPositions.Clear();
            _currentIndex = _summonSnowmanScript.currentIndex;

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
            
            UpdateIconImage();
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

                    if (Mathf.Abs(_targetPositions[i].x - 300f) < Mathf.Epsilon || Mathf.Abs(_targetPositions[i].x + 300f) < Mathf.Epsilon)
                    {
                        rectTransform.anchoredPosition = _targetPositions[i];
                    }
                    else
                    {
                        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, _targetPositions[i], t);
                    }

                    UpdateIconScale(rectTransform);
                }

                yield return null;
            }

            isMoving = false;
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

        private void UpdateIconImage()
        {
            var snowmanListCount = _playerSO.snowmanList.Count;
            if (snowmanListCount < 4) return;
            
            foreach (var icon in skillIcons)
            {
                var rectTransform = icon.GetComponent<RectTransform>();
                if (Mathf.Abs(rectTransform.anchoredPosition.x - 300) < CenterThreshold)
                {
                    icon.GetComponent<Image>().sprite = GetIconSprite(2, snowmanListCount);
                }
                else if (Mathf.Abs(rectTransform.anchoredPosition.x - (-300)) < CenterThreshold)
                {
                    icon.GetComponent<Image>().sprite = GetIconSprite(-2, snowmanListCount);
                }
            }
        }

        private Sprite GetIconSprite(int i, int snowmanListCount)
        {
            if (_playerSO.snowmanList.Count < 1) return null;
            var sum = _currentIndex + i;
            SnowmanType snowmanType;
            if (_playerSO.snowmanList.Count < 2)
            {
                snowmanType = _playerSO.snowmanList[0];
                return Resources.Load<Sprite>("Images/" + snowmanType);
            }
            if (sum < 0)
            { 
                snowmanType = _playerSO.snowmanList[snowmanListCount+sum];
            }
            else if (sum > snowmanListCount - 1)
            {
                snowmanType = _playerSO.snowmanList[sum-snowmanListCount];
            }
            else
            {
                snowmanType = _playerSO.snowmanList[sum];
            }
            return Resources.Load<Sprite>("Images/" + snowmanType);
        }
    }
}
