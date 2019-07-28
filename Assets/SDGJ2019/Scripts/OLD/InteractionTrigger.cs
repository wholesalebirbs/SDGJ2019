using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robots
{

    public enum InteractionTriggerTypes
    {
        OnTrigger,
        OnInput
    }

    [RequireComponent(typeof(BoxCollider))]
    public class InteractionTrigger : MonoBehaviour
    {
        public InteractionTriggerTypes interactionType;

        [SerializeField]
        private Interactable interactable;

        //[HideInInspector]
        public bool active;

        [SerializeField]
        private bool disableAfterTrigger;

        private BoxCollider boxCollider;

        private bool playerInRange;

        public delegate void OnInteractionEvent(InteractionTypes interactionType);
        public static event OnInteractionEvent OnInteractionAvailable;
        public static event OnInteractionEvent OnInteractionUnavailable;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();

            if (interactable == null)
            {
                Debug.Log(name + ": The interactable on this Trigger is null! Disabling gameObject");
            }
        }

        private void Update()
        {
            if (playerInRange)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    TriggerInteraction();

                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!active)
            {
                return;
            }

            // the player is within range to interact;

            playerInRange = true;


            if (interactionType != InteractionTriggerTypes.OnTrigger)
            {
                OnInteractionAvailable?.Invoke(interactable.interactionType);
                return;
            }

            interactable.Interact();

            if (disableAfterTrigger)
            {
                active = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            playerInRange = false;


            OnInteractionUnavailable?.Invoke(interactable.interactionType);
        }

        public void TriggerInteraction()
        {
            if (!active)
            {
                Debug.LogWarning(name + ": this trigger is not active. Not Triggering interaction");
                return;
            }

            if (interactionType != InteractionTriggerTypes.OnInput)
            {
                return;
            }

            interactable.Interact();

            if (disableAfterTrigger)
            {
                active = false;
                OnInteractionUnavailable?.Invoke(interactable.interactionType);
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }

            Color oldColor = Gizmos.color;
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(boxCollider.center + transform.position, boxCollider.size);

            if (interactable)
            {

                Gizmos.DrawLine(transform.position, interactable.transform.position);

            }
            Gizmos.color = oldColor;
        }
#endif
    }
}

