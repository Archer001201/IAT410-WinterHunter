using System.Collections;
using System.Collections.Generic;
using DataSO;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    /*
     * Control game and level states
     */
    public class LevelController : MonoBehaviour
    {
        public List<EnemyCamp> camps;
        public GameObject levelClearedPanel;
        public GameObject asyncSceneLoader;
        // public Slider progressBar;

        private PlayerSO _playerCurrentAttr;
        private PlayerSO _playerDefaultAttr;
        private PlayerAttribute _playerAttr;

        private void Awake()
        {
            _playerCurrentAttr = Resources.Load<PlayerSO>("DataSO/Player_SO");
            _playerDefaultAttr = Resources.Load<PlayerSO>("DataSO/PlayerDefault_SO");
            _playerAttr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        }

        private void Update()
        {
            if (levelClearedPanel == null) return;
            CheckClearedCamp();
        }

        /*
         * Quit game
         */
        public void QuitGame()
        {
            Application.Quit();
        }

        /*
         * Display level cleared panel after all enemies have been killed
         */
        private void CheckClearedCamp()
        {
            var clearedCount = 0;
            foreach (var camp in camps)
            {
                if (camp.isCleared) clearedCount++;
            }
            levelClearedPanel.SetActive(clearedCount == camps.Count);
        }

        /*
         * When click the start game button, initialize player's attributes
         */
        public void ResetPlayerAttributes()
        {
            _playerCurrentAttr.snowmanList.Clear();
            _playerCurrentAttr.maxHealth = _playerDefaultAttr.maxHealth;
            _playerCurrentAttr.maxMana = _playerDefaultAttr.maxMana;
            _playerCurrentAttr.maxStamina = _playerDefaultAttr.maxStamina;
            _playerCurrentAttr.attack = _playerDefaultAttr.attack;
            _playerCurrentAttr.staminaRecovery = _playerDefaultAttr.staminaRecovery;
        }

        public void StartAsyncSceneLoader(string sceneName)
        {
            asyncSceneLoader.SetActive(true);
            asyncSceneLoader.GetComponent<AsyncSceneLoader>().LoadSceneAsync(sceneName);
        }

        public void ReloadCurrentScene()
        {
            asyncSceneLoader.SetActive(true);
            asyncSceneLoader.GetComponent<AsyncSceneLoader>().LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void HealPlayer()
        {
            _playerAttr.health = _playerAttr.maxHealth;
            _playerAttr.stamina = _playerAttr.maxStamina;
            _playerAttr.mana = _playerAttr.maxMana;
        }
    }
}
