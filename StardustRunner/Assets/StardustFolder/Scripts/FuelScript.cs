using System.Collections;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class FuelScript : PickableObject
    {
        public int fuelToAdd;

        protected override void ObjectPicked()
        {
			GameManager.Instance.AddFuel(fuelToAdd);
		}

		protected override void TriggerEnter(GameObject collidingObject)
		{
			// if what's colliding with the coin ain't a characterBehavior, we do nothing and exit
			if (collidingObject.GetComponent<PlayableCharacter>() == null)
				return;

			// adds an instance of the effect at the coin's position
			if (PickEffect != null)
			{
				Instantiate(PickEffect, transform.position,  PickEffect.transform.rotation);
			}

			// if we have a sound manager and if we've specified a song to play when this object is picked
			if (SoundManager.Instance != null && PickSoundFx != null)
			{
				// we play that sound once
				SoundManager.Instance.PlaySound(PickSoundFx, transform.position);
			}

			ObjectPicked();
			// we desactivate the gameobject
			if (!DestroyMode)
				gameObject.SetActive(false);
			else
				Destroy(this.gameObject);
		}
	}
}
