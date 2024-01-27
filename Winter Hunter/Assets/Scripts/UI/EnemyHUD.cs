using System;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHUD : MonoBehaviour
    {
        public Transform trans;
        public Vector3 offset;
        private Camera _mainCamera;
        public RectTransform hudPanel;
        public Image healthFill;
        public Image shieldFill;
        public BasicEnemy enemyAttr;
        public float lerpSpeed = 5f;
        
        private float _targetHealthFill;
        private float _targetShieldFill;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            var worldPosition = trans.position + offset;
            var screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z > 0)
            {
                hudPanel.gameObject.SetActive(true);
                hudPanel.position = screenPosition;
            }
            else
            {
                hudPanel.gameObject.SetActive(false);
            }

            _targetHealthFill = enemyAttr.health / enemyAttr.maxHealth;
            _targetShieldFill = enemyAttr.shield / enemyAttr.maxShield;
            
            healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, _targetHealthFill, lerpSpeed * Time.deltaTime);
            shieldFill.fillAmount = Mathf.Lerp(shieldFill.fillAmount, _targetShieldFill, lerpSpeed * Time.deltaTime);
        }
    }
}
