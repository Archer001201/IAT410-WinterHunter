using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventSystem
{
    public class LevelController : MonoBehaviour
    {
        public string nextLevel;
        public List<EnemyCamp> camps;
        public GameObject levelClearedPanel;

        private void Update()
        {
            CheckClearedCamp();
        }

        public void GoToNextLevel()
        {
            Debug.Log("go to " + nextLevel);
            SceneManager.LoadScene(nextLevel);
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
