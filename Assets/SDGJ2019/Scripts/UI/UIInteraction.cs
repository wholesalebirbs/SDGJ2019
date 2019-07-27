using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Robots
{
    public class UIInteraction : MonoBehaviour
    {
        [SerializeField]
        GameObject interactablePanel;

        [SerializeField]
        TextMeshProUGUI text;

        bool interactionAvailable = false;

        private void Awake()
        {
            InteractionTrigger.OnInteractionAvailable += OnInteractionAvailable;
            InteractionTrigger.OnInteractionUnavailable += OnInteractionUnavailable;

            DialogueManager.OnDialogueStarted += OnDialogueStarted;
            DialogueManager.OnDialogueEnded += OnDialogueEnded;

            interactablePanel.SetActive(false);
        }

        private void OnDestroy()
        {
            InteractionTrigger.OnInteractionAvailable -= OnInteractionAvailable;
            InteractionTrigger.OnInteractionUnavailable -= OnInteractionUnavailable;


            DialogueManager.OnDialogueStarted -= OnDialogueStarted;
            DialogueManager.OnDialogueEnded -= OnDialogueEnded;
        }

        private void OnDialogueStarted()
        {
            interactablePanel.SetActive(false);
        }

        private void OnDialogueEnded()
        {
            if (interactionAvailable)
            {

                interactablePanel.SetActive(true);
            }
        }

        private void OnInteractionAvailable(InteractionTypes interaction)
        {
            interactionAvailable = true;
            interactablePanel.SetActive(true);

            text.text = interaction.ToString();

        }

        private void OnInteractionUnavailable(InteractionTypes interaction)
        {
            interactionAvailable = false;
            interactablePanel.SetActive(false);
        }
    }
}