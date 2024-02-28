using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EventHandler = Utilities.EventHandler;

namespace Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        public Image figure;
        public TextMeshProUGUI avatarName;
        public TextMeshProUGUI dialogueText;
        public GameObject dialogueBox;
        public GameObject continueSign;

        private void Awake()
        {
            continueSign.SetActive(false);
        }

        private void OnEnable()
        {
            EventHandler.OnShowDialoguePiece += ShowDialogueEvent;
        }

        private void OnDisable()
        {
            EventHandler.OnShowDialoguePiece -= ShowDialogueEvent;
        }

        private void ShowDialogueEvent(DialoguePiece piece)
        {
            StartCoroutine(ShowDialogue(piece));
        }

        private IEnumerator ShowDialogue(DialoguePiece piece)
        {
            if (piece != null)
            {
                piece.isDone = false;

                dialogueBox.SetActive(true);
                continueSign.SetActive(false);

                dialogueText.text = string.Empty;

                if (piece.name != string.Empty) avatarName.text = piece.name;
                yield return dialogueText.DOText(piece.dialogueText, 1f).WaitForCompletion();

                piece.isDone = true;
                
               continueSign.SetActive(piece.hasToPause && piece.isDone); 
            }
            else
            {
               dialogueBox.SetActive(false);
            }
        }
    }
}
