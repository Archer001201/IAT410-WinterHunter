using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public UnityEvent onFinishEvent;
        public List<DialoguePiece> dialogueList;

        private Stack<DialoguePiece> _dialogueStack;

        private void Awake()
        {
            FillDialogueStack();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
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
    }
}
