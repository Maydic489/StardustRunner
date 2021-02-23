using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    private bool inAir;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other.gameObject);
    }

    protected virtual void TriggerEnter(GameObject collidingObject)
    {
        if(collidingObject.tag == "Road" && inAir)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (Camera.main.GetComponent<CameraShake>() != null)
            {
                Camera.main.GetComponent<CameraShake>().isShake = true;
                Camera.main.GetComponent<CameraShake>().shakeDuration = 0.2f;
                Invoke("Camera.main.GetComponent<CameraShake>().ResetShakeDuration",0.3f);
            }
            
            inAir = false;
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
            inAir = true;
        }
    }
}
