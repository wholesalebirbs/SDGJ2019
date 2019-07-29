using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Walkman
{
    public class Spawner : MonoBehaviour
    {
        public GameObject prefabToSpawn;

        public int spawnRate = 2;

        [SerializeField]
        Transform[] spawnPositions;

        
        int currentBeat = 0;
        public void OnBeat()
        {
            currentBeat++;
            if (currentBeat >= spawnRate && SongManager.instance.TotalBeats - (SongManager.instance.CurrentBeat - SongManager.instance.FirstBeat)  > 24)
            {
                currentBeat = 0;
                Spawn();
            }            
        }

        private void Spawn()
        {
            int index = Random.Range(0, spawnPositions.Length);
            GameObject.Instantiate(prefabToSpawn, spawnPositions[index].position, Quaternion.identity);
        }
    }
}

