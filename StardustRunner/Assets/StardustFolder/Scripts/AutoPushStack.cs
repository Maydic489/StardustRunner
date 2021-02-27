using System.Collections;
using UnityEngine;


namespace MoreMountains.InfiniteRunnerEngine
{
    public class AutoPushStack : MonoBehaviour
    {
        private bool isBlow;
        private void OnTriggerStay(Collider other)
        {
            TriggerEnter(other.gameObject);
        }

        private void TriggerEnter(GameObject collidingObject)
        {
            if (collidingObject.tag != "Obstacle" && (collidingObject.tag != "Obstacle_Car")) { return; }

            if (this.gameObject.tag == "Obstacle" && collidingObject.tag != "Obstacle_Car") //for stationary obstacle
            {
                if (this.transform.position.z < collidingObject.transform.position.z)
                {
                    return;
                    //this.transform.position -= Vector3.forward * (this.transform.localScale.z / 1.9f);
                }
                else
                {
                    this.transform.position += Vector3.forward * 6f;
                }
            }
            else if (this.gameObject.tag == "Obstacle_Car") //for moving car obstacle
            {
                if (collidingObject.CompareTag("Obstacle_Car"))
                {
                    if (this.transform.position.z < collidingObject.transform.position.z)
                    {
                        return;
                        //this.transform.position -= Vector3.forward * (this.transform.localScale.z / 1.9f);
                    }
                    else
                    {
                        this.transform.position += Vector3.forward * 6f;
                    }
                }
                else if (collidingObject.CompareTag("Obstacle"))
                {
                    if(!isBlow)
                    GetComponent<HurtPlayerOnTouch>().BlowAway();
                    isBlow = true;
                }
            }
        }

        private void OnDisable()
        {
            isBlow = false;
        }
    }
}
