using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robots
{
    public class AudioManager : Singleton<AudioManager>
    {
        /// <summary>
        /// Plays a clip at a position
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        public void PlaySFX(AudioClip clip, Vector3 position)
        {
            if (clip == null)
            {
                Debug.Log("AudioManager: The clip you are trying to play is null!");
            }

            // create a gameobject to play the sound, destroy after clip is finished
            GameObject sfxSourceGO = new GameObject();
            sfxSourceGO.transform.position = position;
            Destroy(sfxSourceGO.gameObject, clip.length);

            AudioSource source = sfxSourceGO.AddComponent<AudioSource>();

            source.clip = clip;
            source.Play();
        }


        /// <summary>
        /// Plays a clip as bgm
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="loop"></param>
        public void PlayBGM(AudioClip clip, bool loop)
        {
            // TODO crossfade, etc
            if (clip == null)
            {
                Debug.Log("AudioManager: The clip you are trying to play is null!");
            }

            AudioSource source = GetComponent<AudioSource>();
            if (source == null)
            {
                gameObject.AddComponent<AudioSource>();
            }

            source.clip = clip;
            source.loop = loop;
            source.Play();
        }
    }

}

