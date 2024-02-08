using System;
using UnityEngine;
using EventHandler = EventSystem.EventHandler;

namespace UISystem
{
    public class MainCanvas : MonoBehaviour
    {
        public GameObject gameOverPanel;

        private void Awake()
        {
            gameOverPanel.SetActive(false);
        }

        private void OnEnable()
        {
            EventHandler.OnPlayerDie += OpenGameOverPanel;
        }

        private void OnDisable()
        {
            EventHandler.OnPlayerDie -= OpenGameOverPanel;
        }

        private void OpenGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }
    }
}
