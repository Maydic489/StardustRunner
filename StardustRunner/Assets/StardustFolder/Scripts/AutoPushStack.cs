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
            if (!collidingObject.CompareTag("Ramp") && collidingObject.tag.Substring(0, 3) != "Obs") { return; }

            if (this.gameObject.CompareTag("Obstacle") && !collidingObject.CompareTag("Obstacle_Car")) //for stationary obstacle
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
                else if (collidingObject.CompareTag("Obstacle") || collidingObject.CompareTag("Ramp"))
                {
                    if(!isBlow)
                        GetComponent<HurtPlayerOnTouch>().BlowAway(collidingObject.transform);
                    isBlow = true;
                }
            }
            else if (collidingObject.CompareTag("Ramp") && this.CompareTag("Item"))
            {
                this.transform.position += Vector3.up * 0.1f;
            }
        }

        private void OnDisable()
        {
            isBlow = false;
        }
    }
}
