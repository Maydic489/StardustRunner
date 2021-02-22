using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other.gameObject);
    }

    protected virtual void TriggerEnter(GameObject collidingObject)
    {
        if(collidingObject.tag == "Road")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit(other.gameObject);
    }

    protected virtual void TriggerExit(GameObject collidingObject)
    {
        if (collidingObject.tag == "Ramp")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
