using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventHandler = Utilities.EventHandler;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public string id;
        public UnityEvent onFinishEvent;
        public List<DialoguePiece> dialogueList;
        public bool isTalking;
        public bool canTalk;

        private InputControls _inputControls;

        private Stack<DialoguePiece> _dialogueStack;

        private void Awake()
        {
            FillDialogueStack();
            _inputControls = new InputControls();
            _inputControls.Gameplay.Interact.performed += _ => { if (canTalk && !isTalking) StartCoroutine(DialogueRoutine()); };
            _inputControls.Gameplay.Skip.performed += _ => EndDialogue();
        }

        private void OnEnable()
        {
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            _inputControls.Disable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canTalk = true;
                EventHandler.ShowInteractableSign(true, "Talk");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canTalk = false;
                EventHandler.ShowInteractableSign(false, "Talk");
            }
        }

        private void FillDialogueStack()
        {
            _dialogueStack = new Stack<DialoguePiece>();
            for (var i = dialogueList.Count - 1; i > -1; i--)
            {
                dialogueList[i].isDone = false;
                _dialogueStack.Push(dialogueList[i]);
            }
        }

        private IEnumerator DialogueRoutine()
        {
            isTalking = true;
            // {
                if (_dialogueStack.TryPop(out var result))
                {
                    EventHandler.ShowDialoguePiece(result);
                    yield return new WaitUntil(() => result.isDone);
                    isTalking = false;
                }
                else
                {
                    EndDialogue();
                    // EventHandler.ShowDialoguePiece(null);
                    // FillDialogueStack();
                    // isTalking = false;
                    //
                    // onFinishEvent?.Invoke();
                    _inputControls.Disable();
                    yield return new WaitForSeconds(1f);
                    _inputControls.Enable();
                    // canTalk = true;
                }
            // }
        }

        private void EndDialogue()
        {
            if (!canTalk) return;
            EventHandler.ShowDialoguePiece(null);
            FillDialogueStack();
            isTalking = false;

            onFinishEvent?.Invoke();
        }

        public void DestroyMe()
        {
            EventHandler.ShowInteractableSign(false, "Talk");
            Destroy(gameObject);
        }
    }
}
