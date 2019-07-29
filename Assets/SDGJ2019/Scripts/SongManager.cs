using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walkman
{
    [RequireComponent(typeof(AudioSource))]
    public class SongManager : MonoBehaviour
    {



        public static SongManager instance;



        [Header("Song Info")]
        [SerializeField]
        private Song _currentSong;
        public Song CurrentSong
        {
            get
            {
                return _currentSong;
            }
        }
        /// <summary>
        /// The current position of the song (in seconds)
        /// </summary>
        [SerializeField]
        private double _songPosition;
        public double SongPosition
        {
            get
            {
                return _songPosition;
            }
        }

        /// <summary>
        /// the current position of the song (in beats)
        /// </summary>
        [SerializeField]
        private int _currentBeat;
        public int CurrentBeat
        {
            get
            {
                return _currentBeat;
            }
        }

        private int lastBeat;

        /// <summary>
        /// The duration of each beat
        /// </summary>
        // [SerializeField]
        private double secPerBeat;




        private int _totalBeats = 0;
        public int TotalBeats
        {
            get
            {
                return _totalBeats;
            }
        }

        private int _currentBPM;
        public int CurrentBPM
        {
            get
            {
                return _currentBPM;
            }
        }

        private int _firstBeat = 0;
        public int FirstBeat
        {
            get
            {
                return _firstBeat;
            }
        }

        private double dspTimeDelta;

        private double lastFrameTime;

        private double currentFrameTime;

        [Header("Time Signature")]
        [SerializeField]
        private int currentBeat;
        [SerializeField]
        private int beatsPerMeasure;


        public double lastBeatTime;
        public double nextBeatTime;
        public float secUntilNextBeat;

        [SerializeField]
        double secPerQuarterBeat;

        [SerializeField]
        int currentSixteenthBeat;

        [SerializeField]
        string currentSixteenthBeatString;
        [SerializeField]
        string[] quarterBeatString = { "1", "e", "and", "a" };

        private AudioSource audioSource;

        [Header("Speed")]
        [SerializeField]
        private float startingSpeedFactor = 1;
        [SerializeField]
        private float _currentSpeedFactor;
        public float CurrentSpeedFactor
        {
            get
            {
                return _currentSpeedFactor;
            }
        }

        private bool songStarted;
        private bool isPlaying;

        public delegate void SongEvent();
        public static event SongEvent OnBeat;
        public static void CallOnbeat()
        {
            OnBeat?.Invoke();
        }

        [Header("Debug Variables")]
        public float changeSpeedTime;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }

            audioSource = GetComponent<AudioSource>();
        }

        public void BeginSong()
        {
            if (_currentSong == null)
            {
                Debug.Log(name + ": There is no song assigned to Song Manager");
                return;
            }

            audioSource.clip = _currentSong.audioClip;

            secPerBeat = 60f / _currentSong.bpm;

            secPerQuarterBeat = secPerBeat / 4;

            songStarted = true;

            isPlaying = true;

            _currentSpeedFactor = startingSpeedFactor;
            _totalBeats = (int)((CurrentSong.audioClip.length / 60f) * _currentSong.bpm * 1.5);

            lastBeat = -1;

            audioSource.Play();
        }


        public void Pause()
        {
            audioSource.Pause();
            isPlaying = false;
        }

        public void Play()
        {
            if (!songStarted)
            {
                BeginSong();
                return;
            }

            isPlaying = true;
            audioSource.UnPause();
        }

        public void ChangeSpeed(float speed)
        {
            audioSource.pitch = speed;
            _currentSpeedFactor = speed;
            _currentBPM = (int)(_currentSong.bpm * speed);
        }

        private void Update()
        {
            if (isPlaying)
            {
                // calculate the position in beats
                _currentBeat = System.Convert.ToInt32(_songPosition / secPerBeat);

                if(_firstBeat == 0)
                {
                    _firstBeat = _currentBeat;
                }

                currentSixteenthBeat = (System.Convert.ToInt32(_songPosition / secPerQuarterBeat) % 4);
                currentSixteenthBeatString = quarterBeatString[currentSixteenthBeat];

                currentSixteenthBeat++;
                if (_currentBeat > lastBeat)
                {
                    lastBeatTime = AudioSettings.dspTime;
                    nextBeatTime = lastBeatTime + secPerBeat;
                    lastBeat = _currentBeat;
                    CallOnbeat();
                }


                currentBeat = 1 + (_currentBeat % beatsPerMeasure);

            }
        }
        void OnAudioFilterRead(float[] data, int channels)
        {
            currentFrameTime = AudioSettings.dspTime;

            dspTimeDelta = currentFrameTime - lastFrameTime;

            if (isPlaying)
            {

                _songPosition += _currentSpeedFactor * dspTimeDelta;
            }

            lastFrameTime = AudioSettings.dspTime;

        }

        public void ChangeSpeedGradually(float newSpeedFactor)
        {
            StopCoroutine(ChangeSpeedCoroutine(newSpeedFactor));
            StartCoroutine(ChangeSpeedCoroutine(newSpeedFactor));

        }

        private IEnumerator ChangeSpeedCoroutine(float newSpeedFactor)
        {
            double elapsedTime = 0;
            float startingSpeedFactor = _currentSpeedFactor;
            while (elapsedTime < changeSpeedTime)
            {
                _currentSpeedFactor = Mathf.Lerp(startingSpeedFactor, newSpeedFactor, ((float)elapsedTime / changeSpeedTime));
                elapsedTime += dspTimeDelta;
                ChangeSpeed(_currentSpeedFactor);
                yield return null;
            }

        }
    }
}
