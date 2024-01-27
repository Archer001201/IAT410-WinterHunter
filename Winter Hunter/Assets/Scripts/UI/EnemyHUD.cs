using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHUD : MonoBehaviour
    {
        public Transform enemyTransform; // 敌人的 Transform
        public Vector3 offset; // 血条相对于敌人头顶的偏移
        public Camera mainCamera; // 游戏的主摄像机
        public RectTransform healthBar; // 血条的 RectTransform
        public Image healthFill; // 血量填充的 Image
        public RectTransform shieldBar; // 血条的 RectTransform
        public Image shieldFill; // 血量填充的 Image
        public BasicEnemy enemyAttr;
        public float lerpSpeed = 5f;
        
        private float _targetHealthFill;
        private float _targetShieldFill;

        private void Update()
        {
            // 计算血条的屏幕位置
            var worldPosition = enemyTransform.position + offset;
            var screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // 更新血条位置并确保其面向摄像机
            if (screenPosition.z > 0) // 确保敌人在摄像机前方
            {
                healthBar.gameObject.SetActive(true);
                healthBar.position = screenPosition;
                shieldBar.gameObject.SetActive(true);
                shieldBar.position = screenPosition;
                // healthBar.rotation = Quaternion.LookRotation(healthBar.position - mainCamera.transform.position);
            }
            else
            {
                healthBar.gameObject.SetActive(false);
                shieldBar.gameObject.SetActive(false);
            }

            // 更新血量显示
            _targetHealthFill = enemyAttr.health / enemyAttr.maxHealth;
            _targetShieldFill = enemyAttr.shield / enemyAttr.maxShield;

            // 平滑过渡到目标填充量
            healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, _targetHealthFill, lerpSpeed * Time.deltaTime);
            shieldFill.fillAmount = Mathf.Lerp(shieldFill.fillAmount, _targetShieldFill, lerpSpeed * Time.deltaTime);
        }
    }
}
