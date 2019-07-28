using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robots
{

    public class FallingObjects : Interactable
    {
        public List<Rigidbody> fallingObjects = new List<Rigidbody>();


        public override void Interact()
        {
            //base.Interact();


            for (int i = 0; i < fallingObjects.Count; i++)
            {
                fallingObjects[i].isKinematic = false;
                fallingObjects[i].useGravity = true;
            }
        }
    }

}