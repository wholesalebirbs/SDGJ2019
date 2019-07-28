using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walkman;

public class LaneBlast : MonoBehaviour
{
    public int blastSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * blastSpeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.gameObject.GetComponent<OnBeatMover>() != null)
        {
            GameObject.Destroy(collider.transform.gameObject);
        }
    }
}
