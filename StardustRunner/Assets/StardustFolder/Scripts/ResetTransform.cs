using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    public Quaternion firstRotation;
    void Start()
    {
        firstRotation = this.transform.rotation;
    }

    public void DoReset()
    {
        Debug.Log("reset");
        this.transform.rotation = firstRotation; 
    }
}
