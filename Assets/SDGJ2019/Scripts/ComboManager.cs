using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    List<Inputs> currentInputs = new List<Inputs>();

    public enum Inputs { Up, Down, Left, Right }

    [System.Serializable]
    public struct Combo
    {
        public Inputs one, two, three, four;

        public UnityEvent comboSuccessEvent;
    }

    public Combo[] combos;

    int maxInputsToShow = 4;
    public void OnValidInput(string direction)
    {
        if (currentInputs.Count >= maxInputsToShow)
        {
            ClearInputs();
        }

        if (direction == "Up")
        {
            currentInputs.Add(Inputs.Up);
        }
        else if (direction == "Down")
        {
            currentInputs.Add(Inputs.Down);
        }
        else if (direction == "Right")
        {
            currentInputs.Add(Inputs.Right);
        }
        else if (direction == "Left")
        {
            currentInputs.Add(Inputs.Left);
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
        foreach(Combo combo in combos)
        {
            Debug.Log(currentInputs[0] + " == " + combo.one);
            if(currentInputs[0] == combo.one && currentInputs[1] == combo.two && currentInputs[2] == combo.three && currentInputs[3] == combo.four)
            {
                Debug.Log("Combo success!");
                combo.comboSuccessEvent?.Invoke();
            }
        }
    }
}
