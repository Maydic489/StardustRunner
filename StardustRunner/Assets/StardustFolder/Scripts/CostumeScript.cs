using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class CostumeScript : MonoBehaviour
    {
        public GameObject PickEffect;
        public void OnDisable()
        {
            if (PickEffect != null && !SimpleLane.isDead && !SimpleLane.isProtect)
            {
                Debug.Log("active invul");
                Instantiate(PickEffect, transform.parent.position, transform.rotation);
            }
        }
    }
}