using System;
using System.Collections;
using System.Collections.Generic;
using DataSO;
using UnityEngine;
using UnityEngine.Events;
using EventHandler = Utilities.EventHandler;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public string id;
        // public UnityEvent onFinishEvent;
        public bool isAppeared = true;
        public int dialogueIndex;
        public List<DialogueList> dialogueLists;
        public bool isTalking;
        public bool canTalk;

        private InputControls _inputControls;
        private GameSO _gameSO;
        private LevelSO _levelSO;

        private Stack<DialoguePiece> _dialogueStack;

        private void Awake()
        {
            id = gameObject.name;
            _inputControls = new InputControls();
            _inputControls.Gameplay.Interact.performed += _ => { if (canTalk && !isTalking) StartCoroutine(DialogueRoutine()); };
            _inputControls.Gameplay.Skip.performed += _ => EndDialogue();
            
            _gameSO = Resources.Load<GameSO>("DataSO/Game_SO");
            _levelSO = _gameSO.currentGameData.levelSo;
            LoadData();
            FillDialogueStack();
            gameObject.SetActive(isAppeared);
        }

        private void OnEnable()
        {
            _inputControls.Enable();
            EventHandler.OnSavingDataAfterDialogue += SaveDialogueData;
            isAppeared = true;
        }

        private void OnDisable()
        {
            _inputControls.Disable();
            EventHandler.OnSavingDataAfterDialogue -= SaveDialogueData;
            isAppeared = false;
        }

        private void Update()
        {
            dialogueIndex = Mathf.Clamp(dialogueIndex, 0, dialogueLists.Count);
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
            if (dialogueLists.Count < 1) return;
            _dialogueStack = new Stack<DialoguePiece>();
            var list = dialogueLists[dialogueIndex].dialogueList;
            for (var i = list.Count - 1; i > -1; i--)
            {
                list[i].isDone = false;
                _dialogueStack.Push(list[i]);
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
            dialogueLists[dialogueIndex].onFinishEvent?.Invoke();
            
            EventHandler.ShowDialoguePiece(null);
            FillDialogueStack();
            isTalking = false;
            
            EventHandler.SaveDataAfterDialogue();
        }

        public void DestroyMe()
        {
            EventHandler.ShowInteractableSign(false, "Talk");
            Destroy(gameObject);
        }

        public void ChangeDialogueIndex(int index)
        {
            dialogueIndex = index;
        }

        private void LoadData()
        {
            var dialogue = _levelSO.dialogueEvents.Find(dialogue => dialogue.id == id);
            if (dialogue == null)
            {
                _levelSO.dialogueEvents.Add(new DialogueEventData
                {
                    id = this.id,
                    isAppeared = this.isAppeared,
                    dialogueIndex = 0
                });
            }
            else
            {
                isAppeared = dialogue.isAppeared;
                dialogueIndex = dialogue.dialogueIndex;
            }
        }

        private void SaveDialogueData()
        {
            var dialogue = _levelSO.dialogueEvents.Find(dialogue => dialogue.id == id);
            if (dialogue == null) return;

            dialogue.dialogueIndex = dialogueIndex;
            dialogue.isAppeared = isAppeared;
        }
    }
}
