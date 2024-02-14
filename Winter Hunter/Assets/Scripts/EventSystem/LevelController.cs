using System;
using System.Collections.Generic;
using DataSO;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventSystem
{
    /*
     * Control game and level states
     */
    public class LevelController : MonoBehaviour
    {
        public List<EnemyCamp> camps;
        public GameObject levelClearedPanel;

        private PlayerSO _playerAttr;
        private PlayerSO _playerDefaultAttr;

        private void Awake()
        {
            _playerAttr = Resources.Load<PlayerSO>("DataSO/Player_SO");
            _playerDefaultAttr = Resources.Load<PlayerSO>("DataSO/PlayerDefault_SO");
        }

        private void Update()
        {
            if (levelClearedPanel == null) return;
            CheckClearedCamp();
        }

        /*
         * Load next scene
         */
        public void GoToNextLevel(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
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
            _playerAttr.snowmanList.Clear();
            _playerAttr.maxHealth = _playerDefaultAttr.maxHealth;
            _playerAttr.maxEnergy = _playerDefaultAttr.maxEnergy;
            _playerAttr.maxStamina = _playerDefaultAttr.maxStamina;
            _playerAttr.attack = _playerDefaultAttr.attack;
            _playerAttr.staminaRecovery = _playerDefaultAttr.staminaRecovery;
        }
    }
}
