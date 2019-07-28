using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Walkman
{
    public class OnBeatSubscriber : MonoBehaviour
    {
        public UnityEvent OnBeat;

        private void OnEnable()
        {
            SongManager.OnBeat += OnBeatCallback;
        }

        private void OnDisable()
        {
            SongManager.OnBeat -= OnBeatCallback;
        }

        protected virtual void OnBeatCallback()
        {
            OnBeat.Invoke();
        }
    }
}
