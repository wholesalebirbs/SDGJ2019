using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBlaster : MonoBehaviour
{
    public GameObject laneBlasterPrefab;

    public Transform[] lanes;
    
    public void BlastLane(int index)
    {
        GameObject.Instantiate(laneBlasterPrefab, lanes[index].position, Quaternion.identity);
    }
}
