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
        public UnityEvent onFinishEvent;
        public List<DialoguePiece> dialogueList;
        public bool isTalking;
        public bool canTalk;

        private Stack<DialoguePiece> _dialogueStack;

        private void Awake()
        {
            FillDialogueStack();
        }

        private void OnTriggerEnter(Collider other)
        {
            canTalk = other.CompareTag("Player");
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
            {
                if (_dialogueStack.TryPop(out var result))
                {
                    EventHandler.ShowDialoguePiece(result);
                    yield return new WaitUntil(() => result.isDone);
                    isTalking = false;
                }
            }
        }
    }
}
