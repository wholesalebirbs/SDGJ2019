using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robots
{
    public class NPC : Interactable
    {
        [SerializeField]
        DialogueSO dialogue;

        public override void Interact()
        {
            if (!canInteract)
            {
                return;
            }
            DialogueManager.Instance.StartDialogue(dialogue);
        }

    }

}