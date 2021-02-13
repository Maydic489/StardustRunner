using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private bool isDone;
    void Update()
    {
        if(transform.position.z <= 4 && !isDone)
        {
            GetComponent<Animation>().Play("Spike");
            isDone = true;
        }
    }

    private void OnEnable()
    {
        GetComponent<Animation>().Play("Spike_idle");
        isDone = false;
    }
}
