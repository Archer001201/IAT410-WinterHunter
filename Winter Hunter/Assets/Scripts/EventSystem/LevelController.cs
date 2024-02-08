using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventSystem
{
    public class LevelController : MonoBehaviour
    {
        public List<EnemyCamp> camps;
        public GameObject levelClearedPanel;

        private void Update()
        {
            if (levelClearedPanel == null) return;
            CheckClearedCamp();
        }

        public void GoToNextLevel(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void CheckClearedCamp()
        {
            var clearedCount = 0;
            foreach (var camp in camps)
            {
                if (camp.isCleared) clearedCount++;
            }
            levelClearedPanel.SetActive(clearedCount == camps.Count);
        }
    }
}
