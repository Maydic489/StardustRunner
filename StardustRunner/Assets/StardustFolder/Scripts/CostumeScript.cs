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
            if (PickEffect != null && !SimpleLane.isDead)
            {
                Instantiate(PickEffect, transform.parent.position, transform.rotation);
                LevelManager.Instance.ActivateInvul(2f);
            }
        }
    }
}