using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robots
{
    public enum InteractionTypes
    {
        Interact,
        Talk,

    }

    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField]
        public InteractionTypes interactionType;

        [SerializeField]
        protected bool canInteract;

        protected virtual void Awake()
        {
            DialogueManager.OnDialogueStarted += OnDialogueStarted;
            DialogueManager.OnDialogueEnded += OnDialogueEnded;
        }

        protected virtual void OnDestroy()
        {
            DialogueManager.OnDialogueStarted -= OnDialogueStarted;
            DialogueManager.OnDialogueEnded -= OnDialogueEnded;
        }


        protected virtual void OnDialogueStarted()
        {
            canInteract = false;
        }

        protected virtual void OnDialogueEnded()
        {
            canInteract = true;
        }


        public virtual void Interact()
        {
            if (!canInteract)
            {
                return;
            }
        }
    }
}

