using System.Collections;
using UnityEngine;

public class AutoPushStack : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        TriggerEnter(other.gameObject);
    }

    private void TriggerEnter(GameObject collidingObject)
    {
        if (collidingObject.tag != "Obstacle") { return; }

        if(this.transform.position.z < collidingObject.transform.position.z)
        {
            return;
            //this.transform.position -= Vector3.forward * (this.transform.localScale.z / 1.9f);
        }
        else
        {
            this.transform.position += Vector3.forward*2f;
        }
    }
}
