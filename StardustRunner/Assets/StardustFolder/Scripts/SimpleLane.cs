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
		public float gameSpeed;
		public GameObject riderModel;
		public GameObject bikeModel;
		public GameObject shadow;
		public GameObject groundPivot;
		public GameObject helmetModel;
		public float MoveSpeed = 5f;
		private float slowSpeed = 0.1f;
		public GameObject mainCamera;
		public static Vector3 playerPositoin;
		public float slideDirection;
		private float oldDirection;
		public static char whatLane;
		public static bool isDead;
		public static bool isInvul;
		public static bool isProtect;
		private bool isSlide;
		private bool lookBack;
		public Quaternion bikeDefault;
		public static bool isSpeed {get; set;}
		static int s_BlinkingValueHash;

		protected override void Start()
        {
			mainCamera = GameObject.Find("Main Camera");
			s_BlinkingValueHash = Shader.PropertyToID("_BlinkingValue");
			Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);
			isDead = false;
			isInvul = false;
			isSpeed = false;
			isProtect = false;

			bikeDefault = bikeModel.transform.rotation;
		}

        protected override void Update()
		{
			// we determine the distance between the ground and the Jumper
			ComputeDistanceToTheGround();
			// we send our various states to the animator.      
			UpdateAnimator();
			// if we're supposed to reset the player's position, we lerp its position to its initial position
			ResetPosition();
			// we check if the player is out of the death bounds or not
			CheckDeathConditions();

			IsOutofFuel();

			ChooseLane();

			//old way to move, not good for rigidbody
			//if(transform.position.x != slideDirection)
			//transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.deltaTime);

			//if (groundPivot.GetComponent<Rigidbody>().transform.rotation.z != (slideDirection * 10))
			//{
			//    groundPivot.GetComponent<Rigidbody>().transform.Rotate(new Vector3(0, 0, (-100 * slideDirection) * Time.deltaTime));
			//}

			if (!groundPivot.GetComponent<Animation>().IsPlaying("Anim_Slide") && isSlide)
            {
				isSlide = false;
				GetComponent<CapsuleCollider>().enabled = true;
			}

			//go back to normal pose after changing lane
			if ((IsBetween(transform.position.x,slideDirection-0.2f,slideDirection+0.2f)) && !groundPivot.GetComponent<Animation>().IsPlaying("Anim_Slide"))
			{
				if ((groundPivot.GetComponent<Animation>().IsPlaying("Anim_RotateLeft") || groundPivot.GetComponent<Animation>().IsPlaying("Anim_RotateRight")))
					CenterPose();
				else
				{
					groundPivot.GetComponent<Animation>().IsPlaying("Anim_Idle");
				}

			}

			if(GameManager.Instance.FuelPoints < 40 && !groundPivot.GetComponent<Animation>().isPlaying && !lookBack)
            {
				//groundPivot.GetComponent<Animation>().Play("Anim_LookBack");
				groundPivot.GetComponent<Animation>()["Anim_LookBack"].layer = 1;
				groundPivot.GetComponent<Animation>().Play("Anim_LookBack");
				groundPivot.GetComponent<Animation>()["Anim_LookBack"].weight = 0.4f;
				lookBack = true;
            }
			else if(GameManager.Instance.FuelPoints > 40)
            {
				lookBack = false;
            }

			playerPositoin = transform.position;
			gameSpeed = LevelManager.Instance.Speed;
		}

		private void FixedUpdate()
		{
			if (!IsBetween(transform.position.x, slideDirection - 0.05f, slideDirection + 0.05f))
			{
				if(slowSpeed < MoveSpeed)
                {
					slowSpeed += slowSpeed * (0.05f+slowSpeed);
                }
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), slowSpeed * Time.deltaTime);
			}
			else
            {
				slowSpeed = 0.25f;
            }

			//tips: use physic to move game obj with rigidbody
			//if (GetComponent<Rigidbody>().transform.position.x != slideDirection)
			//GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.fixedDeltaTime));
		}

		public void ChooseLane()
        {
            if (transform.position.x > -0.3f && transform.position.x < 0.3f)
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
				return;
            }
		}

		public override void LeftStart()
		{
			Debug.Log("Swipe left");
			oldDirection = slideDirection;
            //_rigidbodyInterface.AddForce(Vector3.left * MoveSpeed * Time.deltaTime);
            if (whatLane == 'r' && slideDirection != 0f)
            {
                slideDirection = 0f;
                if (!isSlide)
                    groundPivot.GetComponent<Animation>().Play("Anim_RotateLeft");
            }
            else
            {
                slideDirection = -1.6f;
                if (!isSlide)
                    groundPivot.GetComponent<Animation>().Play("Anim_RotateLeft");
            }
		}

		public override void RightStart()
		{
			Debug.Log("Swipe Right");
			oldDirection = slideDirection;
			//_rigidbodyInterface.AddForce(Vector3.right * MoveSpeed * Time.deltaTime);
			if (whatLane == 'l' && slideDirection != 0f)
			{
				slideDirection = 0f;
				if (!isSlide)
					groundPivot.GetComponent<Animation>().Play("Anim_RotateRight");
			}
			else
			{
				slideDirection = 1.6f;
				if (!isSlide)
					groundPivot.GetComponent<Animation>().Play("Anim_RotateRight");
			}
		}

        public override void DownStart()
        {
			isSlide = true;
			GetComponent<CapsuleCollider>().enabled = false;
			groundPivot.GetComponent<Animation>().Play("Anim_Slide");
		}

        public void PreActivateInvul(float duration)
        {
			StopAllCoroutines();
			StartCoroutine(ActivateInvul(duration));
		}

		private void CenterPose()
        {
			if (groundPivot.GetComponent<Animation>().IsPlaying("Anim_RotateLeft"))
			{
				groundPivot.GetComponent<Animation>().Play("Anim_LeftToCenter");
			}
			else if (groundPivot.GetComponent<Animation>().IsPlaying("Anim_RotateRight"))
			{
				groundPivot.GetComponent<Animation>().Play("Anim_RightToCenter");
			}
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

		public void ToggleProtect(bool state)
        {
			isProtect = state;
			helmetModel.SetActive(state);

			if(!state)
            {
				LevelManager.Instance.ActivateInvul(2f);
				Camera.main.GetComponent<CameraShake>().isShake = true;
			}
        }

		public void IsOutofFuel()
        {
			if(GameManager.Instance.FuelPoints <= 0f && !isDead)
            {
				if (!isInvul)
					if(!isProtect)
						LevelManager.Instance.KillCharacter(this);
					else
						LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().ToggleProtect(false);
			}
        }

		protected override void CheckDeathConditions()
		{
			if (LevelManager.Instance.CheckDeathCondition(GetPlayableCharacterBounds()) && !isDead)
			{
				LevelManager.Instance.KillCharacter(this);
			}
		}

		public override void Die()
		{
			//Destroy(bikeModel);
			Camera.main.GetComponent<CameraShake>().isShake = true;
			isDead = true;
			groundPivot.GetComponent<Animation>().Stop();
			GameManager.Instance.SlowMotion();
			Destroy(shadow);
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.GetComponent<BoxCollider>().enabled = false;
			this.GetComponent<CapsuleCollider>().enabled = false;
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

		public void HurtPlayer(bool pushing)
		{
			GameManager.Instance.AddFuel(-50f);
			if (GameManager.Instance.FuelPoints > 0.00f) { LevelManager.Instance.ActivateInvul(1f); }
			Camera.main.GetComponent<CameraShake>().isShake = true;

			if (pushing)
			{
				if (transform.position.x <= oldDirection)
					RightStart();
				else
					LeftStart();
			}
		}

		public bool IsBetween(float testValue, float min, float max)
		{	
			if(testValue > min && testValue < max)
            {
				return (true);
            }
			else
            {
				return (false);
            }
		}
	}
}
