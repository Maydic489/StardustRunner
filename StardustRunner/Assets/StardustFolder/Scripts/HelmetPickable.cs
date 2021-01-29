using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class HelmetPickable : PickableObject
    {
        protected override void ObjectPicked()
        {
            if (LevelManager.Instance == null)
            {
                return;
            }

            LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().ToggleProtect(true);
        }
    }
}
