using DataSO;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    /*
     * Control player's HUD
     */
    public class PlayerHUD : MonoBehaviour
    {
        public Image healthBar;
        public Image staminaBar;
        public Image energyBar;
        public float lerpSpeed = 5f;
        
        private PlayerSO _playerSO;
        private PlayerAttribute _playerAttr;
        private float _targetHealthFill;
        private float _targetStaminaFill;
        private float _targetEnergyFill;

        private void Start()
        {
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
            _playerSO = Resources.Load<PlayerSO>("DataSO/Player_SO");
        }

        private void Update()
        {
            _targetHealthFill = _playerAttr.health / _playerSO.maxHealth;
            _targetStaminaFill = _playerAttr.stamina / _playerSO.maxStamina;
            _targetEnergyFill = _playerAttr.energy / _playerSO.maxEnergy;
            
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, _targetHealthFill, lerpSpeed * Time.deltaTime);
            staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, _targetStaminaFill, lerpSpeed * Time.deltaTime);
            energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, _targetEnergyFill, lerpSpeed * Time.deltaTime);
        }
    }
}
