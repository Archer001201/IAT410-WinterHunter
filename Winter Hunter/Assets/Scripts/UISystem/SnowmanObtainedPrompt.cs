using System;
using System.Collections;
using System.Collections.Generic;
using DataSO;
using Snowman;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using EventHandler = Utilities.EventHandler;

namespace UISystem
{
    public class SnowmanObtainedPrompt : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI type;
        public TextMeshProUGUI prompt;
        private PlayerSO _playerSO;
        private SnowmanSO _snowmanSO;
        private readonly Dictionary<SnowmanType, SnowmanLevel> _snowmenPlayerHas = new();
        private RectTransform _trans;
        public float maxHeight = 200f;
        public float animationDuration = 1f;
        public float displayDuration = 2f;

        private void Awake()
        {
            _trans = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            EventHandler.OnShowSnowmanObtainedPrompt += ShowSnowmanObtainedPrompt;
        }

        private void OnDisable()
        {
            EventHandler.OnShowSnowmanObtainedPrompt -= ShowSnowmanObtainedPrompt;
        }

        private void ShowSnowmanObtainedPrompt(SnowmanTypeAndLevel typeAndLevel)
        {
            Debug.Log("show prompt");
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
            _snowmanSO = Resources.Load<SnowmanSO>("DataSO/SnowmanSO/" + typeAndLevel.type + "_SO");
            icon.sprite = _snowmanSO.icon;
            type.text = _snowmanSO.type.ToString();
            foreach (var snowman in _playerSO.snowmanList)
            {
                _snowmenPlayerHas.Add(snowman.type, snowman.level);
            }

            if (_snowmenPlayerHas.TryGetValue(typeAndLevel.type, out var level))
            {
                prompt.text = level == SnowmanLevel.Basic ? "New snowman unlocked!" : "Snowman upgraded!";
            }

            StartCoroutine(AdjustHeight());
        }

        private IEnumerator AdjustHeight()
        {
            // 首先，从0增长到目标高度
            const float startHeight = 0; // 开始高度应为0
            float time = 0;

            while (time < animationDuration)
            {
                var currentHeight = Mathf.Lerp(startHeight, maxHeight, time / animationDuration);
                _trans.sizeDelta = new Vector2(_trans.sizeDelta.x, currentHeight);
                time += Time.deltaTime;
                yield return null;
            }

            _trans.sizeDelta = new Vector2(_trans.sizeDelta.x, maxHeight);

            // 停留1秒
            yield return new WaitForSeconds(displayDuration);

            // 然后，从目标高度缩小至0
            time = 0; // 重置时间
            while (time < animationDuration)
            {
                var currentHeight = Mathf.Lerp(maxHeight, 0, time / animationDuration);
                _trans.sizeDelta = new Vector2(_trans.sizeDelta.x, currentHeight);
                time += Time.deltaTime;
                yield return null;
            }

            _trans.sizeDelta = new Vector2(_trans.sizeDelta.x, 0);

            // 最后，设置GameObject为不激活
            _snowmenPlayerHas.Clear();
            _trans.gameObject.SetActive(false);
        }
    }
}
