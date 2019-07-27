using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robots
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        public string objectName;
        public string[] sentences;
    }
}

