using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class HurtPlayerOnTouch : KillsPlayerOnTouch
	{
		public bool DontAutoCheckLane;
		public bool isGround;
		public bool isBreakable;
		public bool isWeak;
		public char whatLane;
		private bool isBreak;
		public List<GameObject> showModel;
		public GameObject breakablePart;
		public GameObject breakableCopy;
		public GameObject crashEffect;

		private void OnEnable()
		{
			if (!DontAutoCheckLane)
				Invoke("CheckLane", 1);

			foreach (GameObject obj in showModel)
			{
				obj.SetActive(true);
			}

			if (isBreakable)
			{
				breakableCopy = Instantiate(breakablePart, this.transform.position, this.transform.rotation, this.transform);
				isBreak = false;
			}
		}
		protected override void TriggerEnter(GameObject collidingObject)
		{
			// we verify that the colliding object is a PlayableCharacter with the Player tag. If not, we do nothing.			
			if (collidingObject.tag != "Player") { return; }

			PlayableCharacter player = collidingObject.GetComponent<PlayableCharacter>();
			if (player == null) { return; }

			// we ask the LevelManager to kill the character
			if (!SimpleLane.isInvul && !isWeak)
			{
				if (!SimpleLane.isProtect)
				{
					if((SimpleLane.whatLane != this.whatLane) || this.whatLane == 'n')
					{
						LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().HurtPlayer(!isGround);				}
					else
						LevelManager.Instance.KillCharacter(player);
				}
				else
					LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().ToggleProtect(false);

			}

			if(isWeak)
            {
				if(SimpleLane.isWheelie)
                {
					if (isBreakable && !isBreak)
					{
						foreach(GameObject obj in showModel)
                        {
							obj.SetActive(false);
						}

						breakableCopy.SetActive(true);
						breakableCopy.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

						foreach (Rigidbody rb in breakableCopy.GetComponent<RagdollDeathScript>().ragdollBodies)
						{
							rb.AddExplosionForce(20f, new Vector3(transform.position.x, 0, 0), 5f, 3f, ForceMode.Impulse);
						}
						foreach (Collider collider in breakableCopy.GetComponent<RagdollDeathScript>().ragdollColliders)
						{
							Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<BoxCollider>(), collider, true);
							Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<CapsuleCollider>(), collider, true);
						}

						// adds an instance of the effect at the coin's position
						if (crashEffect != null)
						{
							Instantiate(crashEffect, this.transform.position+(Vector3.forward*0.5f), crashEffect.transform.rotation);
						}

						Destroy(breakableCopy, 2);
						isBreak = true;
					}
				}
				else
					LevelManager.Instance.KillCharacter(player);
			}
		}

		public void CheckLane()
		{
			if (transform.position.x > -0.1f && transform.position.x < 0.1f)
			{
				whatLane = 'm';
			}
			else if (transform.position.x < -0.1f)
			{
				whatLane = 'l';
			}
			else if (transform.position.x > 0.1f)
			{
				whatLane = 'r';
			}
			else
			{
				whatLane = 'n';
			}
		}

	}
}
