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
		public char whatLane; //l,m,r for one lane object, n for hurt object, k for kill object with no specific lane
		private bool isBreak;
		[SerializeField]
		private bool isHit;
		public List<GameObject> showModel;
		public GameObject breakablePart;
		public GameObject breakableCopy;
		public GameObject crashEffect;
		public AudioSource breakSFX;

		private void OnEnable()
		{
			isHit = false;
			if (!DontAutoCheckLane)
				Invoke("CheckLane", 1);

			foreach (GameObject obj in showModel)
			{
				obj.SetActive(true);
			}

			if (isBreakable && breakableCopy == null)
			{
				breakableCopy = Instantiate(breakablePart, this.transform.position, this.transform.rotation, this.transform);
				isBreak = false;
			}
		}

		protected override void OnTriggerEnter(Collider other)
		{
			if ((other.GetType() == typeof(BoxCollider) || other.GetType() == typeof(CapsuleCollider)) && !isHit)
				TriggerEnter(other.gameObject);
		}
		protected override void TriggerEnter(GameObject collidingObject)
		{
			// we verify that the colliding object is a PlayableCharacter with the Player tag. If not, we do nothing.			
			if (collidingObject.tag != "Player") { return; }

			PlayableCharacter player = collidingObject.GetComponent<PlayableCharacter>();
			if (player == null) { return; }

			// we ask the LevelManager to kill the character
			if (!SimpleLane.isInvul)
			{
				if (!SimpleLane.isProtect)
				{
					//check if do small damage instead of instant kill
					if (((SimpleLane.whatLane != this.whatLane) || this.whatLane == 'n') && this.whatLane != 'k')
					{
						if (GameManager.Instance.Status != GameManager.GameStatus.GameOver)
						{
							if (this.whatLane != 'n')
								LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().HurtPlayer(!isGround, this.transform);
							else if (this.whatLane == 'n')
							{
								Debug.Log("hit N");
								LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().HurtPlayer(!isGround, null);
							}
						}
						//if(crashEffect != null)
						//Instantiate(crashEffect, this.transform.position - (collidingObject.transform.position/0.75f)+(Vector3.forward*0.5f), crashEffect.transform.rotation,this.transform);
					}
					else if(SimpleLane.isWheelie && isBreakable && !isBreak)
					{ BreakingDown(); }
					else
						LevelManager.Instance.KillCharacter(player);
				}
				else
				{
					if (SimpleLane.isWheelie && isBreakable && !isBreak)
					{ BreakingDown(); }
					else if(SimpleLane.isProtect)
						{
						LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().ToggleProtect(false);
						if (isBreakable && !isBreak) { BreakingDown(); }
						if (this.CompareTag("Obstacle_Car")) {BlowAway(collidingObject.transform);}
					}
				}

			}
			else
			{
				if (isBreakable && !isBreak)
				{
					BreakingDown();
				}

				if (this.CompareTag("Obstacle_Car"))
				{
					GameManager.Instance.AddPoints(100);
					BlowAway(collidingObject.transform);
				}
			}

			isHit = true;
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

		public void BreakingDown()
		{
			foreach (GameObject obj in showModel)
			{
				obj.SetActive(false);
			}

			breakableCopy.SetActive(true);
			breakableCopy.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

			foreach (Rigidbody rb in breakableCopy.GetComponent<RagdollDeathScript>().ragdollBodies)
			{
				rb.AddExplosionForce(20f, new Vector3(transform.position.x, 0, transform.position.z-2), 10f, 3f, ForceMode.Impulse);
			}
			foreach (Collider collider in breakableCopy.GetComponent<RagdollDeathScript>().ragdollColliders)
			{
				Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<BoxCollider>(), collider, true);
				Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<CapsuleCollider>(), collider, true);
			}

			if (crashEffect != null)
			{
				Instantiate(crashEffect, this.transform.position+Vector3.up*0.5f, crashEffect.transform.rotation,this.transform);
			}

			if(breakSFX != null)
				SoundManager.Instance.PlaySoundSource(breakSFX, transform.position);

			Invoke("CountDownDestroy", 5);
			isBreak = true;
		}

		public void BlowAway(Transform collidingObject)
		{
			this.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

			foreach (Rigidbody rb in this.GetComponent<RagdollDeathScript>().ragdollBodies)
			{
				rb.AddExplosionForce(20f, this.transform.position -((this.transform.position-collidingObject.transform.position)/2), 10f, 3f, ForceMode.Impulse);
			}
			//not sure if need, copied from SimpleLane, so, leave it here.
			//if (GameManager.Instance.Status != GameManager.GameStatus.GameOver) //check if game over
			//{
			//	foreach (Collider collider in this.GetComponent<RagdollDeathScript>().ragdollColliders)
			//	{
			//		if (LevelManager.Instance.CurrentPlayableCharacters[0] != null)
			//		{
			//			Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<BoxCollider>(), collider, true);
			//			Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<CapsuleCollider>(), collider, true);
			//		}
			//	}
			//}

			if (crashEffect != null)
			{
				Instantiate(crashEffect, (this.transform.position - ((this.transform.position - collidingObject.transform.position) / 2))+ Vector3.up * 0.5f, crashEffect.transform.rotation);
			}

			if (breakSFX != null)
				SoundManager.Instance.PlaySoundSource(breakSFX, transform.position);

			if (gameObject.transform.Find("Shadow") != null)
				gameObject.transform.Find("Shadow").gameObject.SetActive(false);

			Invoke("CountDownRecycle", 1);
		}

		private void CountDownRecycle()
		{
			GetComponent<MMPoolableObject>().Destroy();
		}

		private void CountDownDestroy()
		{
			Destroy(breakableCopy);
		}

		private void OnDisable()
		{
			if (gameObject.transform.Find("Shadow") != null)
				gameObject.transform.Find("Shadow").gameObject.SetActive(true);

			isHit = false;
			if (isBreak)
			{
				CancelInvoke("CountDownDestroy");
				Destroy(breakableCopy);
			}
		}

	}
}
