using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float bpm = 60;
    [Range(2,20)]
    public float cubeSnappiness = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentBeat = (float)AudioSettings.dspTime * bpm / 60;
        float t = Mathf.Pow(currentBeat % 1, cubeSnappiness);
        
        this.transform.rotation = Quaternion.Euler((t + (int)currentBeat) * 360 / 4, 0, 0);
        this.transform.position = new Vector3(0, Mathf.Sqrt(1 - Mathf.Pow(((t + 1) * 2) % 2 - 1,2)) * 0.207f + 0.5f, 0);//* Mathf.Abs((Time.time * 60 / bpm) % 1 - 0.5f) * Mathf.Sqrt(2) / 2 + 0.5f, 0);
    }
}
