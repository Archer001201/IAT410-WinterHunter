using System;
using UnityEngine;
using EventHandler = EventSystem.EventHandler;

namespace UISystem
{
    public class MainCanvas : MonoBehaviour
    {
        public GameObject gameOverPanel;
        public GameObject levelClearedPanel;
        public GameObject optionsMenu;

        private InputControls _inputControls;

        private void Awake()
        {
            gameOverPanel.SetActive(false);
            levelClearedPanel.SetActive(false);
            optionsMenu.SetActive(false);
            _inputControls = new InputControls();
            _inputControls.Gameplay.EscButton.performed += _=> HandleOptionsMenu();
        }

        private void OnEnable()
        {
            EventHandler.OnPlayerDie += OpenGameOverPanel;
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            EventHandler.OnPlayerDie -= OpenGameOverPanel;
            _inputControls.Disable();
        }

        private void OpenGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }

        public void HandleOptionsMenu()
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);
        }
    }
}
