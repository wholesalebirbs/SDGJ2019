using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Robots
{
    public class DialogueManager : Singleton<DialogueManager>
    { 
        [SerializeField]
        TextMeshProUGUI nameText;

        [SerializeField]
        TextMeshProUGUI dialogueText;

        private Queue<string> sentences = new Queue<string>();
        public Animator animator;

        public delegate void DialogueEvent();
        public static event DialogueEvent OnDialogueStarted;
        public static event DialogueEvent OnDialogueEnded;

        public void StartDialogue(DialogueSO dialogue)
        {

            OnDialogueStarted?.Invoke();

            animator.SetBool("IsOpen", true);

            nameText.text = dialogue.objectName;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }



            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            animator.SetBool("IsOpen", false);

            OnDialogueEnded?.Invoke();
        }

    }

}
