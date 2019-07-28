using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    List<string> currentInputs = new List<string>();

    static string[] combo1 = { "Up", "Down", "Left", "Left" };
    static string[] combo2 = { "Right", "Left", "Right", "Left" };

    string[][] combos = { combo1, combo2 };

    int maxInputsToShow = 4;
    public void OnValidInput(string direction)
    {
        if (currentInputs.Count >= maxInputsToShow)
        {
            ClearInputs();
        }

        if (direction == "Up")
        {
            currentInputs.Add("Up");
        }
        else if (direction == "Down")
        {
            currentInputs.Add("Down");
        }
        else if (direction == "Right")
        {
            currentInputs.Add("Right");
        }
        else if (direction == "Left")
        {
            currentInputs.Add("Left");
        }

        if (currentInputs.Count == maxInputsToShow)
        {
            CheckForCombos();
        }
    }

    public void OnInvalidInput()
    {
        ClearInputs();
    }

    private void ClearInputs()
    {
        currentInputs.Clear();
    }

    private void CheckForCombos()
    {
        foreach(string[] combo in combos)
        {
            int i = 0;
            int numCorrect = 0;
            foreach(string input in combo)
            {
                if(currentInputs[i++].Equals(input))
                {
                    numCorrect++;
                }
            }

            if(numCorrect == maxInputsToShow)
            {
                Debug.Log("Combo success!");
            }
        }
    }
}
