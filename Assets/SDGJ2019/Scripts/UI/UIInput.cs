using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walkman
{

    public class UIInput : MonoBehaviour
    {
        public GameObject upPrefab;
        public GameObject downPrefab;
        public GameObject rightPrefab;
        public GameObject leftPrefab;
        List<GameObject> currentInputs = new List<GameObject>();

        int maxInputsToShow = 4;
        public void OnValidInput(string direction)
        {
            if (currentInputs.Count >= maxInputsToShow)
            {
                ClearInputs();
            }

            if (direction == "Up")
            {
                GameObject go = Instantiate(upPrefab, transform);
                currentInputs.Add(go);
            }
            else if (direction == "Down")
            {
                GameObject go = Instantiate(downPrefab, transform);
                currentInputs.Add(go);
            }
            else if (direction == "Right")
            {
                GameObject go = Instantiate(rightPrefab, transform);
                currentInputs.Add(go);
            }
            else if (direction == "Left")
            {
                GameObject go = Instantiate(leftPrefab, transform);
                currentInputs.Add(go);
            }
        }
        public void OnInvalidInput()
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            foreach (GameObject go in currentInputs)
            {
                Destroy(go);
            }
            currentInputs.Clear();
        }
    }
}
