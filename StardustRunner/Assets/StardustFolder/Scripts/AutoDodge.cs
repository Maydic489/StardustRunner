using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class AutoDodge : SpawnerMover
    {
        public GameObject navigator;
        private char thisSafeLane;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {
            GetSlideDirection();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        public override void GetSlideDirection()
        {
            thisSafeLane = navigator.GetComponent<LaneNavigator>().safeLane;
            if (thisSafeLane == 'l')
            {
                slideDirection = -1.6f;
            }
            else if (thisSafeLane == 'm')
            {
                slideDirection = 0;
            }
            else if (thisSafeLane == 'r')
            {
                slideDirection = 1.6f;
            }
        }
    }
}
