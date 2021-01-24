using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	/// <summary>
	/// Extends playable character to implement the specific gameplay of the Dragon level
	/// </summary>
	public class SimpleLane : PlayableCharacter
	{
		public GameObject riderModel;
		//public GameObject riderCollider;
		public GameObject bikeModel;
		public GameObject shadow;
		public float MoveSpeed = 5f;
		public GameObject mainCamera;
		public static Vector3 playerPositoin;
		private float slideDirection;
		private char whatLane;
		public static bool isDead;
		public static bool isInvul;
		public bool invulStatus;
		static int s_BlinkingValueHash;

		protected override void Start()
        {
			mainCamera = GameObject.Find("Main Camera");
			s_BlinkingValueHash = Shader.PropertyToID("_BlinkingValue");
			Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);
			isDead = false;
			isInvul = false;
        }

        protected override void Update()
		{
			invulStatus = isInvul;
			// we determine the distance between the ground and the Jumper
			ComputeDistanceToTheGround();
			// we send our various states to the animator.      
			UpdateAnimator();
			// if we're supposed to reset the player's position, we lerp its position to its initial position
			ResetPosition();
			// we check if the player is out of the death bounds or not
			CheckDeathConditions();

			ChooseLane();

			if(transform.position.x != slideDirection)
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.deltaTime);

			if(!isDead)
				mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(slideDirection*0.8f, mainCamera.transform.position.y, mainCamera.transform.position.z), (MoveSpeed*0.8f) * Time.deltaTime);
			//else
				//mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, riderModel.GetComponent<Collider>().transform.position, (MoveSpeed * 0.8f) * Time.deltaTime);

			playerPositoin = transform.position;
		}

		public void ChooseLane()
        {
			if (transform.position.x > -1f && transform.position.x < 1f)
			{
				whatLane = 'm';
			}
			else if (transform.position.x < 0)
			{
				whatLane = 'l';
			}
			else if (transform.position.x > 0)
			{
				whatLane = 'r';
			}
		}

		public override void LeftStart()
		{
			//_rigidbodyInterface.AddForce(Vector3.left * MoveSpeed * Time.deltaTime);
			if (whatLane == 'r')
			{
				slideDirection = 0f;
			}
			else
            {
				slideDirection = -1.5f;
            }
		}

		public override void RightStart()
		{
			//_rigidbodyInterface.AddForce(Vector3.right * MoveSpeed * Time.deltaTime);
			if (whatLane == 'l')
			{
				slideDirection = 0f;
			}
			else
			{
				slideDirection = 1.5f;
			}
		}

		public void PreActivateInvul(float duration)
        {
			StopAllCoroutines();
			StartCoroutine(ActivateInvul(duration));
		}
		public IEnumerator ActivateInvul(float duration)
		{
			isInvul = true;

			float time = 0;
			float currentBlink = 1.0f;
			float lastBlink = 0.0f;
			const float blinkPeriod = 0.1f;

			while (time < duration && isInvul)
			{
				Shader.SetGlobalFloat(s_BlinkingValueHash, currentBlink);

				// We do the check every frame instead of waiting for a full blink period as if the game slow down too much
				// we are sure to at least blink every frame.
				// If blink turns on and off in the span of one frame, we "miss" the blink, resulting in appearing not to blink.
				yield return null;
				time += Time.deltaTime;
				lastBlink += Time.deltaTime;

				if (blinkPeriod < lastBlink)
				{
					lastBlink = 0;
					currentBlink = 1.0f - currentBlink;
				}
			}

			Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);

			//yield return new WaitForSeconds(duration);
			isInvul = false;
		}

		protected override void CheckDeathConditions()
		{
			if ((LevelManager.Instance.CheckDeathCondition(GetPlayableCharacterBounds()) || GameManager.Instance.FuelPoints <= 0f) && !isDead)
			{
				LevelManager.Instance.KillCharacter(this);
			}
		}

		public override void Die()
		{
			Debug.Log("die");
			//Destroy(bikeModel);
			isDead = true;
			GameManager.Instance.SlowMotion();
			Destroy(shadow);
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.GetComponent<Collider>().enabled = false;
			riderModel.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);
			bikeModel.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

            foreach (Rigidbody rb in riderModel.GetComponent<RagdollDeathScript>().ragdollBodies)
            {
                rb.AddExplosionForce(30f, new Vector3(transform.position.x + Random.Range(-0.7f, 0.7f), 0, -1f), 5f, 3f, ForceMode.Impulse);
            }

            Rigidbody bikeRb = bikeModel.GetComponent<Rigidbody>();
			bikeRb.AddExplosionForce(25f, new Vector3(transform.position.x + Random.Range(-0.7f,0.7f), -0.5f, -1f), 5f, 1f, ForceMode.Impulse);
			foreach (Collider collider in riderModel.GetComponent<RagdollDeathScript>().ragdollColliders)
			{
				Physics.IgnoreCollision(bikeModel.GetComponent<Collider>(),collider,true);
			}
		}
	}
}
