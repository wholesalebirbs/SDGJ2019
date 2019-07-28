using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBlast : MonoBehaviour
{
    public int blastSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * blastSpeed);
    }
}
