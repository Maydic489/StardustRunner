using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class InvulPick : PickableObject
    {
        public float invulDuration;
        protected override void ObjectPicked()
        {
            LevelManager.Instance.ActivateInvul(invulDuration);
        }
    }
}