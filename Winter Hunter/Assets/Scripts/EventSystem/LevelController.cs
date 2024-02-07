using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventSystem
{
    public class LevelController : MonoBehaviour
    {
        public string nextLevel;
        public void GoToNextLevel()
        {
            Debug.Log("go to " + nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
