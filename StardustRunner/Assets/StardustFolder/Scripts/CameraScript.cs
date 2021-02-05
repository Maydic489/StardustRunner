using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class CameraScript : MonoBehaviour
    {
        public float moveSpeed = 5;
        public float zoomBoost;

        // Update is called once per frame
        void Update()
        {
            if (SimpleLane.isSpeed && GetComponent<Camera>().fieldOfView < zoomBoost)
            {
                    GetComponent<Camera>().fieldOfView++;
            }
            else if(!SimpleLane.isSpeed && GetComponent<Camera>().fieldOfView > 65)
            {
                    GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 0.5f;
            }
        }
    }
}