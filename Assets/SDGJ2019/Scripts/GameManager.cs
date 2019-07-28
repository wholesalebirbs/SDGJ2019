using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walkman;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    SongManager songManager;

    // Start is called before the first frame update
    void Start()
    {
        songManager.ChangeSpeed(1.5f);
        songManager.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
