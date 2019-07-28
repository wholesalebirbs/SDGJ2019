using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walkman
{
    [RequireComponent(typeof(OnBeatSubscriber))]
    public class OnBeatMover : MonoBehaviour
    {
        public Vector3 direction;

        public int beatsPerMovement;

        private int currentBeats;

        public void OnBeat()
        {
            currentBeats++;
            if (currentBeats >= beatsPerMovement)
            {
                currentBeats = 0;

                Move();
            }
        }

        public void Move()
        {
            transform.Translate(direction);
        }
    }

}