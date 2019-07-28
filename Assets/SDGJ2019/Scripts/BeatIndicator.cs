using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{

    [SerializeField]
    Animator animator;
   public void OnBeat()
    {
        animator.SetTrigger("OnBeat");
    }
}
