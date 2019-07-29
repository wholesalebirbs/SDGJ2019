using Robots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    List<Inputs> currentInputs = new List<Inputs>();

    public AudioClip oneClip;
    public AudioClip twoClip;
    public AudioClip threeClip;
    public AudioClip heyClip;
    public AudioClip awwClip;

    public enum Inputs { Up, Down, Left, Right }

    [System.Serializable]
    public struct Combo
    {
        public Inputs one, two, three, four;

        public UnityEvent comboSuccessEvent;

        public AudioClip comboAudio;
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

        if(currentInputs.Count == 1)
        {
            AudioManager.instance.PlaySFX(oneClip, Vector3.zero);
        } 
        else if (currentInputs.Count == 2)
        {
            AudioManager.instance.PlaySFX(twoClip, Vector3.zero);
        }
        else if (currentInputs.Count == 3)
        {
            AudioManager.instance.PlaySFX(threeClip, Vector3.zero);
        }
        else if (currentInputs.Count == 4)
        {
            AudioManager.instance.PlaySFX(heyClip, Vector3.zero);
        }
    }

    public void OnInvalidInput()
    {
        if (currentInputs.Count == 3)
        {
            AudioManager.instance.PlaySFX(awwClip, Vector3.zero);
        }
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
            if(currentInputs[0] == combo.one && currentInputs[1] == combo.two && currentInputs[2] == combo.three && currentInputs[3] == combo.four)
            {
                combo.comboSuccessEvent?.Invoke();
                AudioManager.instance.PlaySFX(combo.comboAudio, Vector3.zero);
            }
        }
    }
}
