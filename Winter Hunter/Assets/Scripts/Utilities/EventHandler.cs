using System;
using Dialogue;
using Snowman;

namespace Utilities
{
    /*
     * Observer pattern
     */
    public static class EventHandler
    {
        /*
         * Notice existed snowman to destroy itself
         */
        public static event Action OnDestroyExistedSnowman;

        public static void DestroyExistedSnowman()
        {
            OnDestroyExistedSnowman?.Invoke();
        }
        
        /*
         * Notice player a snowman chest has been opened
         */
        public static event Action<SnowmanTypeAndLevel> OnOpenSnowmanChest;

        public static void OpenSnowmanChest(SnowmanTypeAndLevel snowman)
        {
            OnOpenSnowmanChest?.Invoke(snowman);
        }

        /*
         * Notice skill panel to update ui display
         */
        public static event Action OnUpdateSkillPanel;

        public static void UpdateSkillPanel()
        {
            OnUpdateSkillPanel?.Invoke();
        }

        /*
         * Notice game player has been died
         */
        public static event Action OnPlayerDie;

        public static void PlayerDie()
        {
            OnPlayerDie?.Invoke();
        }

        public static event Action<bool> OnSetGameplayActionMap;

        public static void SetGameplayActionMap(bool isActive)
        {
            OnSetGameplayActionMap?.Invoke(isActive);
        }

        public static event Action<SnowmanTypeAndLevel, bool> OnShowSnowmanDetail;

        public static void ShowSnowmanDetail(SnowmanTypeAndLevel typeAndLevel, bool isUnlocked)
        {
            OnShowSnowmanDetail?.Invoke(typeAndLevel, isUnlocked);
        }

        public static event Action<SnowmanTypeAndLevel> OnShowSnowmanObtainedPrompt;

        public static void ShowSnowmanObtainedPrompt(SnowmanTypeAndLevel typeAndLevel)
        {
            OnShowSnowmanObtainedPrompt?.Invoke(typeAndLevel);
        }
        
        public static event Action<SnowmanTypeAndLevel> OnOpenSnowmanObtainedPrompt;

        public static void OpenSnowmanObtainedPrompt(SnowmanTypeAndLevel typeAndLevel)
        {
            OnOpenSnowmanObtainedPrompt?.Invoke(typeAndLevel);
        }
        
        public static event Action<DialoguePiece> OnShowDialoguePiece;

        public static void ShowDialoguePiece(DialoguePiece dialoguePiece)
        {
            OnShowDialoguePiece?.Invoke(dialoguePiece);
        }
    }
}
