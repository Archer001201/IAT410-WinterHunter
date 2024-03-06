using Snowman;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

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
        public GameObject inventoryPanel;
        public GameObject snowmanObtainedPrompt;
        public GameObject teleportPanel;

        private InputControls _inputControls;

        private void Awake()
        {
            gameOverPanel.SetActive(false);
            levelClearedPanel.SetActive(false);
            optionsMenu.SetActive(false);
            snowmanObtainedPrompt.SetActive(false);
            _inputControls = new InputControls();
            _inputControls.Global.OptionButton.performed += _=> HandlePanel(optionsMenu);
            _inputControls.Global.InventoryButton.performed += _ => HandlePanel(inventoryPanel);
        }

        private void OnEnable()
        {
            EventHandler.OnPlayerDie += OpenGameOverPanel;
            EventHandler.OnOpenSnowmanObtainedPrompt += OpenSnowmanObtainedPrompt;
            EventHandler.OnOpenTeleportPanel += HandleTeleportPanel;
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            EventHandler.OnPlayerDie -= OpenGameOverPanel;
            EventHandler.OnShowSnowmanObtainedPrompt -= OpenSnowmanObtainedPrompt;
            EventHandler.OnOpenTeleportPanel -= HandleTeleportPanel;
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
        // public void HandleOptionsMenu()
        // {
        //     if (optionsMenu.activeSelf)
        //     {
        //         optionsMenu.SetActive(false);
        //         _inputControls.Gameplay.Enable();
        //         _inputControls.UI.Disable();
        //     }
        //     else
        //     {
        //         optionsMenu.SetActive(true);
        //         _inputControls.Gameplay.Disable();
        //         _inputControls.UI.Enable();
        //     }
        // }
        //
        // private void HandleInventoryPanel()
        // {
        //     if (inventoryPanel.activeSelf)
        //     {
        //         inventoryPanel.SetActive(false);
        //         _inputControls.Gameplay.Enable();
        //         _inputControls.UI.Disable();
        //     }
        //     else
        //     {
        //         inventoryPanel.SetActive(true);
        //         _inputControls.Gameplay.Disable();
        //         _inputControls.UI.Enable();
        //     }
        // }

        public void HandlePanel(GameObject panel)
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
                EventHandler.SetGameplayActionMap(true);
            }
            else
            {
                panel.SetActive(true);
                EventHandler.SetGameplayActionMap(false);
            }
        }

        private void OpenSnowmanObtainedPrompt(SnowmanTypeAndLevel snowman)
        {
            if (snowmanObtainedPrompt == null)
            {
                Debug.Log("prompt null");
                return;
            }
            snowmanObtainedPrompt.SetActive(true);
            EventHandler.ShowSnowmanObtainedPrompt(snowman);
        }

        private void HandleTeleportPanel(bool isOpen, string nextLevel, string prompt)
        {
            teleportPanel.SetActive(isOpen);
            var teleportScript = teleportPanel.GetComponent<TeleportPanel>();
            teleportScript.nextLevel = nextLevel;
            teleportScript.prompt = prompt;
        }
    }
}
