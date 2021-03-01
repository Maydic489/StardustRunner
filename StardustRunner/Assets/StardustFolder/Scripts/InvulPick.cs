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
            Debug.Log("Give Coke");
            LevelManager.Instance.TemporarilyMultiplySpeed(SpeedFactor, EffectDuration, "item");
            LevelManager.Instance.ActivateInvul(invulDuration);
            LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().PreSuperman();
        }
    }
}