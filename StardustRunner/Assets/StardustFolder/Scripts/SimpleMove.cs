using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public Vector3 moveValue;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(moveValue*Time.deltaTime);
    }
}
