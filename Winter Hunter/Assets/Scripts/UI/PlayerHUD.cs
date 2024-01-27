using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        public Image healthBar;
        public Image staminaBar;
        public Image energyBar;
        public float lerpSpeed = 5f;
        
        private PlayerAttribute _playerAttr;
        private float _targetHealthFill;
        private float _targetStaminaFill;
        private float _targetEnergyFill;

        private void Start()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        }

        private void Update()
        {
            // 更新目标填充量
            _targetHealthFill = _playerAttr.health / _playerAttr.maxHealth;
            _targetStaminaFill = _playerAttr.stamina / _playerAttr.maxStamina;
            _targetEnergyFill = _playerAttr.charge / _playerAttr.maxCharge;

            // 平滑过渡到目标填充量
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, _targetHealthFill, lerpSpeed * Time.deltaTime);
            staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, _targetStaminaFill, lerpSpeed * Time.deltaTime);
            energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, _targetEnergyFill, lerpSpeed * Time.deltaTime);
        }
    }
}
