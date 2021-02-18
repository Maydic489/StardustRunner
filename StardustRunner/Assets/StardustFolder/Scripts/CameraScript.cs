using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class CameraScript : MonoBehaviour
    {
        public float moveSpeed = 5;
        public float zoomBoost;
        public float zoomWheelie;

        // Update is called once per frame
        void Update()
        {
            if (SimpleLane.isSpeed && SimpleLane.isSuperman && GetComponent<Camera>().fieldOfView < zoomBoost)
            {
                    GetComponent<Camera>().fieldOfView += 0.5f;
            }
            else if (SimpleLane.isSpeed && SimpleLane.isWheelie && GetComponent<Camera>().fieldOfView < zoomWheelie)
            {
                    GetComponent<Camera>().fieldOfView += 0.5f;
            }
            else if(!SimpleLane.isSpeed && !SimpleLane.isSuperman && !SimpleLane.isWheelie && GetComponent<Camera>().fieldOfView > 65)
            {
                    GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 0.25f;
            }
        }
    }
}