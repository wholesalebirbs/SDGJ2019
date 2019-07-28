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

        public float bpm = 120;

        private Vector3 startPosition;
        private float startBeat;


        public void Start()
        {
            startPosition = this.transform.position;
            startBeat = (float)AudioSettings.dspTime * bpm / 60;
        }

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

        
        //void Update()
        //{
        //    float currentBeat = (float)AudioSettings.dspTime * bpm / 60;

        //    float t = Mathf.Pow(currentBeat % 1, 5);

        //    float formulizedT = Mathf.Sqrt(1 - Mathf.Pow((((t) + 1) * 2) % 2 - 1, 2));

        //    Vector3 nextPosition = startPosition + (1 + (int)(currentBeat - startBeat)) * direction;

        //    this.transform.position = Vector3.Lerp(nextPosition - direction, nextPosition, formulizedT);
        //}
    }

}