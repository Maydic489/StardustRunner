using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class SpikeScript : MonoBehaviour
    {
        private bool isDone;
        void Update()
        {
            if (this.transform.position.z <= (LevelManager.Instance.Speed)/6.5f && !isDone)
            {
                GetComponent<Animation>().Play("Spike");
                isDone = true;
            }
        }

        private void OnEnable()
        {
            GetComponent<Animation>().Play("Spike_idle");
            isDone = false;
        }
    }
}
