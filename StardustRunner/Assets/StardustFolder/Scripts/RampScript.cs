using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class RampScript : MonoBehaviour
	{
		public AudioClip jumpSFX;
		public bool isJump;

        private void OnEnable()
        {
			isJump = false;
        }
        private void OnCollisionEnter(Collision other)
		{
			//if (!isJump)
			//	TriggerEnter(other.gameObject);
		}

        private void OnTriggerEnter(Collider other)
        {
			if (!isJump)
				TriggerEnter(other.gameObject);
		}

        protected virtual void TriggerEnter(GameObject collidingObject)
		{
			if (collidingObject.tag != "Player") { return; }
			isJump = true;
			if (!SimpleLane.isSpeed)
			{
				Debug.Log("normal boost");
				collidingObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, 0), ForceMode.Impulse);
			}
			else
			{
				Debug.Log("double boost");
				LevelManager.Instance.TemporarilyMultiplySpeed(1.5f, 0.5f, "ramp");
				collidingObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 18, 0), ForceMode.Impulse);
			}

			if(jumpSFX != null)
				SoundManager.Instance.PlaySound(jumpSFX, transform.position);
		}
	}
}
