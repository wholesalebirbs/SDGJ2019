using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Walkman
{
    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {

    }
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        SongManager songManager;

        [SerializeField]
        int maxInputCount;

        public List<string> inputList = new List<string>();

        public float inputPadding = .1f;

        public bool canKey = false;


        public StringEvent OnValidInput;

        public UnityEvent OnInvalidInput;

        private int lastInputBeat = -1;

        private void Update()
        {
            if (AudioSettings.dspTime < songManager.lastBeatTime + inputPadding || AudioSettings.dspTime > songManager.nextBeatTime - inputPadding)
            {
                canKey = true;
            }
            else
            {
                canKey = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (canKey && lastInputBeat != songManager.CurrentBeat)
                {
                    OnValidInput.Invoke(AddInputToList("Up"));
                    lastInputBeat = songManager.CurrentBeat;
                }
                else
                {
                    OnInvalidInput.Invoke();
                    ClearInputs();
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (canKey && lastInputBeat != songManager.CurrentBeat)
                {
                    OnValidInput.Invoke(AddInputToList("Down"));
                    lastInputBeat = songManager.CurrentBeat;
                }
                else
                {
                    OnInvalidInput.Invoke();
                    ClearInputs();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (canKey && lastInputBeat != songManager.CurrentBeat)
                {
                    OnValidInput.Invoke(AddInputToList("Left"));
                    lastInputBeat = songManager.CurrentBeat;
                }
                else
                {
                    OnInvalidInput.Invoke();
                    ClearInputs();
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (canKey && lastInputBeat != songManager.CurrentBeat)
                {
                    OnValidInput.Invoke(AddInputToList("Right"));
                    lastInputBeat = songManager.CurrentBeat;
                }
                else
                {
                    OnInvalidInput.Invoke();
                    ClearInputs();
                }
            }
        }

        private string AddInputToList(string input)
        {
            if (inputList.Count >= maxInputCount)
            {
                ClearInputs();
            }

            inputList.Add(input);

            return input;
        }


        private void ClearInputs()
        {
            inputList.Clear();
            
        }


        private void OnAudioFilterRead(float[] data, int channels)
        {
  
        }
    }

}