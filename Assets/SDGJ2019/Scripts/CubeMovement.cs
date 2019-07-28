using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float bpm = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler((float)AudioSettings.dspTime * 60 / bpm * 360 / 4, 0, 0);
        this.transform.position = new Vector3(0, Mathf.Sqrt(1 - Mathf.Pow(((((float)AudioSettings.dspTime * 60 / bpm) + 1) * 2) % 2 - 1,2)) * 0.207f + 0.5f, 0);//* Mathf.Abs((Time.time * 60 / bpm) % 1 - 0.5f) * Mathf.Sqrt(2) / 2 + 0.5f, 0);
    }
}
