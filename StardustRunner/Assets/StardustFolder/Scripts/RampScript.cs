using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class RampScript : MonoBehaviour
	{
		private bool isJump;

        private void OnEnable()
        {
			isJump = false;
        }
        private void OnCollisionEnter(Collision other)
		{
			if(!isJump)
			TriggerEnter(other.gameObject);
		}

		protected virtual void TriggerEnter(GameObject collidingObject)
		{
			if (collidingObject.tag != "Player") { return; }
			isJump = true;
			if (!SimpleLane.isSpeed)
				collidingObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15, 0), ForceMode.Impulse);
			else
				collidingObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 18, 0), ForceMode.Impulse);
		}
	}
}
