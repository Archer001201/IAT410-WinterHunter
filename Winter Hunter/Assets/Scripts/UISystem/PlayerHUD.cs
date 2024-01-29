using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
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
            _targetHealthFill = _playerAttr.health / _playerAttr.maxHealth;
            _targetStaminaFill = _playerAttr.stamina / _playerAttr.maxStamina;
            _targetEnergyFill = _playerAttr.charge / _playerAttr.maxCharge;
            
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, _targetHealthFill, lerpSpeed * Time.deltaTime);
            staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, _targetStaminaFill, lerpSpeed * Time.deltaTime);
            energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, _targetEnergyFill, lerpSpeed * Time.deltaTime);
        }
    }
}
