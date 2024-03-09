using System.Collections;
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
        public GameObject savingData;

        private InputControls _inputControls;
        private Coroutine _showDataCoroutine;

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
            EventHandler.OnShowSavingData += StartShowingData;
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            EventHandler.OnPlayerDie -= OpenGameOverPanel;
            EventHandler.OnShowSnowmanObtainedPrompt -= OpenSnowmanObtainedPrompt;
            EventHandler.OnOpenTeleportPanel -= HandleTeleportPanel;
            EventHandler.OnShowSavingData -= StartShowingData;
            _inputControls.Disable();
        }

        /*
         * Open game over panel
         */
        private void OpenGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }

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

            if (panel == inventoryPanel)
            {
                inventoryPanel.GetComponent<InventoryPanel>().UpdateSnowmanCells();
            }
        }

        private void OpenSnowmanObtainedPrompt(SnowmanTypeAndLevel snowman)
        {
            if (snowmanObtainedPrompt == null)
            {
                // Debug.Log("prompt null");
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

        private void StartShowingData()
        {
            _showDataCoroutine ??= StartCoroutine(ShowSavingData());
        }

        private void StopShowingData()
        {
            if (_showDataCoroutine == null) return;
            StopCoroutine(_showDataCoroutine);
            _showDataCoroutine = null;
        }

        private IEnumerator ShowSavingData()
        {
            if (savingData.activeSelf) yield return null;
            else
            {
                savingData.SetActive(true);

                yield return new WaitForSeconds(2f);
                
                savingData.SetActive(false);
                StopShowingData();
            }
        }
    }
}
