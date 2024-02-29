using System;
using UnityEngine;
using Utilities;
using EventHandler = Utilities.EventHandler;

namespace UISystem
{
    public class TeleportPanel : MonoBehaviour
    {
        public string nextLevel;
        public GameObject asyncSceneLoader;

        private void OnEnable()
        {
            EventHandler.AllowMouseInput(false);
        }

        private void OnDisable()
        {
            EventHandler.AllowMouseInput(true);
        }

        public void GoToNextScene()
        {
            asyncSceneLoader.SetActive(true);
            asyncSceneLoader.GetComponent<AsyncSceneLoader>().LoadSceneAsync(nextLevel);
        }
    }
}
