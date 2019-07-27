using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robots
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField]
        DialogueSO dialogue;

        bool canInteract = true;


        private void Awake()
        {
            DialogueManager.OnDialogueStarted += OnDialogueStarted;
            DialogueManager.OnDialogueEnded += OnDialogueEnded;
        }

        private void OnDestroy()
        {
            DialogueManager.OnDialogueStarted -= OnDialogueStarted;
            DialogueManager.OnDialogueEnded -= OnDialogueEnded;
        }
        public void Interact()
        {
            if (!canInteract)
            {
                return;
            }
            DialogueManager.Instance.StartDialogue(dialogue);
        }


        private void OnDialogueStarted()
        {
            canInteract = false;
        }

        private void OnDialogueEnded()
        {
            canInteract = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interact();
            }
        }
    }

}