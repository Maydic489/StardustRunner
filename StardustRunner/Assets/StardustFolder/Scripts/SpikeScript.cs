using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private bool isDone;
    void Update()
    {
        if(transform.position.z <= 10 && !isDone)
        {
            GetComponent<Animation>().Play("Spike");
            isDone = true;
        }
    }
}
