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
            if (this.transform.position.z <= (LevelManager.Instance.Speed)/1.5f && !isDone) //old value is 6.5f
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
