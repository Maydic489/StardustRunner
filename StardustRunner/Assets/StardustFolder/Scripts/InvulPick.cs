using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class InvulPick : PickableObject
    {
        public float invulDuration;
        public float SpeedFactor = 2f;
        public float EffectDuration = 5f;
        protected override void ObjectPicked()
        {
            if (LevelManager.Instance == null)
            {
                return;
            }

            LevelManager.Instance.TemporarilyMultiplySpeed(SpeedFactor, EffectDuration);
            LevelManager.Instance.ActivateInvul(invulDuration);
        }
    }
}