using UnityEngine;
using EventHandler = EventSystem.EventHandler;

namespace UISystem
{
    /*
     * Control main canvas
     */
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

        /*
         * Open game over panel
         */
        private void OpenGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }

        /*
         * Handle option menu
         */
        public void HandleOptionsMenu()
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);
        }
    }
}
