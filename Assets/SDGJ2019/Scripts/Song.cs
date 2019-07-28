using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walkman
{
    [CreateAssetMenu(fileName = "Song", menuName = "WholesaleBirbs/Scriptable Objects/Rhythm/Song")]
    public class Song : ScriptableObject
    {
        public AudioClip audioClip;

        public float bpm;
    }
}

