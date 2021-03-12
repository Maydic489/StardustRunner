using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
		public GameObject riderRagdoll;
		public GameObject bikeRagdoll;
		public GameObject ragdollObj;
		public GameObject shadow;
		public GameObject groundPivot;
		private Animation pivotAnim;
		public GameObject helmetModel;
		public GameObject headContainer;
		public GameObject crashEffect;
		public List<ParticleSystem> boostEffect;
		public ParticleSystem slideEffect;
		public float MoveSpeed = 5f;
		public static float turnSpeedMultiply = 1;
		private float slowSpeed = 0.1f;
		public GameObject mainCamera;
		public static Vector3 playerPositoin;
		public float slideDirection;
		private float oldDirection;
		public static char whatLane;
		public char inLane;
		public static bool isDead;
		public static bool isInvul;
		public static bool isBoost;
		public static bool isProtect;
		public static bool isSuperman;
		private bool isSlide;
		public static bool isWheelie;
		private bool lookBack;

		private static bool animationOn = true;
		public static bool isSpeed {get; set;}
		
		static int s_BlinkingValueHash;

		protected override void Start()
        {
			mainCamera = GameObject.Find("Main Camera");
			s_BlinkingValueHash = Shader.PropertyToID("_BlinkingValue");
			pivotAnim = groundPivot.GetComponent<Animation>();
			Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);
			ResetStaticBool();
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
			
			CheckAnimation();

			inLane = whatLane;

			//play animation when get speed boost
			//CheckSpeedBoost();

			//old way to move, not good for rigidbody
			//if(transform.position.x != slideDirection)
			//transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.deltaTime);

			//if (groundPivot.GetComponent<Rigidbody>().transform.rotation.z != (slideDirection * 10))
			//{
			//    groundPivot.GetComponent<Rigidbody>().transform.Rotate(new Vector3(0, 0, (-100 * slideDirection) * Time.deltaTime));
			//}

			LookBackCheck();

			playerPositoin = transform.position;
			gameSpeed = LevelManager.Instance.Speed;
		}

		private void FixedUpdate()
		{
            if (!IsBetween(transform.position.x, slideDirection - 0.05f, slideDirection + 0.05f))
            {
                if (slowSpeed < MoveSpeed)
                {
                    slowSpeed += slowSpeed * (0.05f + slowSpeed);
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), (slowSpeed * Time.deltaTime)*turnSpeedMultiply); //last number is for how fast for switching lane
            }
            else
            {
                slowSpeed = 0.25f;
            }

            //go back to normal pose after changing lane
            if ((IsBetween(transform.position.x, slideDirection - 0.2f, slideDirection + 0.2f)) && !pivotAnim.IsPlaying("Anim_Slide") && !isDead)
            {
                if (pivotAnim.IsPlaying("Anim_RotateLeft") || pivotAnim.IsPlaying("Anim_RotateRight"))
                    CenterPose();
                else if (!pivotAnim.IsPlaying("Anim_LeftToCenter") && !pivotAnim.IsPlaying("Anim_RightToCenter"))
                {
                    //pivotAnim.IsPlaying("Anim_Idle");
                }
            }

            //go back to normal pose after changing lane
            if ((IsBetween(transform.position.x, slideDirection - 0.2f, slideDirection + 0.2f)) && !pivotAnim.IsPlaying("Anim_Slide") && !isDead)
            {
                if (pivotAnim.IsPlaying("Anim_RotateLeft") || pivotAnim.IsPlaying("Anim_RotateRight"))
                    CenterPose();
                else if (!pivotAnim.IsPlaying("Anim_LeftToCenter") && !pivotAnim.IsPlaying("Anim_RightToCenter"))
                {
                    //pivotAnim.IsPlaying("Anim_Idle");
                }
            }
        }

		public void ChooseLane()
        {
            if (transform.position.x > -0.7f && transform.position.x < 0.7f)
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
        #region Control
        public override void LeftStart()
		{
			oldDirection = slideDirection;
            //_rigidbodyInterface.AddForce(Vector3.left * MoveSpeed * Time.deltaTime);
            if (whatLane == 'r' && slideDirection != 0f)
            {
                slideDirection = 0f;
                if (!isSlide && !isWheelie && animationOn)
                    PlayTurnAnim("Left");
            }
            else
            {
                slideDirection = -1.6f;
                if (!isSlide && !isWheelie && whatLane != 'l' && animationOn)
                    PlayTurnAnim("Left");
            }
		}

		public override void RightStart()
		{
			oldDirection = slideDirection;
			//_rigidbodyInterface.AddForce(Vector3.right * MoveSpeed * Time.deltaTime);
			if (whatLane == 'l' && slideDirection != 0f)
			{
				slideDirection = 0f;
                if (!isSlide && !isWheelie && animationOn)
                    PlayTurnAnim("Right");
            }
			else
			{
				slideDirection = 1.6f;
                if (!isSlide && !isWheelie && whatLane != 'r' && animationOn)
                    PlayTurnAnim("Right");
            }
		}

        public override void DownStart()
        {
			if (!isSuperman && !isWheelie && !isSlide)
			{
				isSlide = true;
				GetComponent<CapsuleCollider>().enabled = false;
				pivotAnim.Play("Anim_Slide");

				if(transform.position.y <0.5f) //make sure is on ground
                {
					//var emission = slideEffect.emission;
					//emission.enabled = true;
				}
			}
		}

        public override void UpStart()
        {
			if (!isSuperman && !isSlide && !isWheelie)
			{
				isWheelie = true;
                pivotAnim["Bike_Wheelie"].layer = 3;
                pivotAnim.Play("Bike_Wheelie");
                pivotAnim["Bike_Wheelie"].weight = 0.4f;

				var emission = boostEffect[3].emission;
				emission.enabled = true;
				Invoke("StopWindFX", 0.5f);

				LevelManager.Instance.TemporarilyMultiplySpeed(1.5f, 0.5f, "wheelie");
			}
		}

        #endregion
        //public void CheckSpeedBoost()
        //      {
        //	if (isSpeed && !isSuperman && isInvul && !isWheelie) { PlaySupermanAnim(isSpeed); }
        //	else if (!isSpeed && isSuperman) { PlaySupermanAnim(isSpeed); }
        //}

        public void PreSuperman()
        {
			StartCoroutine(PlaySupermanAnim());
        }

        public IEnumerator PlaySupermanAnim()
        {
			
			{
				if (!isSuperman)
				{
					isSuperman = true;
					pivotAnim["Anim_Superman"].layer = 1;
					pivotAnim["Anim_Superman"].speed = 1;
					pivotAnim.Play("Anim_Superman");
					pivotAnim["Anim_Superman"].weight = 0.4f;
				}
				Camera.main.GetComponent<CameraShake>().isShake = true;
				Camera.main.GetComponent<CameraShake>().shakeDuration = 5;

				for (int i = 0;i<boostEffect.Count;i++)
                {
					var emission = boostEffect[i].emission;
					emission.enabled = true;
                }
			}
			yield return new WaitForSeconds(5);
            {
				isSuperman = false;
				pivotAnim["Anim_Superman"].layer = 1;
				pivotAnim["Anim_Superman"].speed = -1;
				pivotAnim["Anim_Superman"].time = pivotAnim["Anim_Superman"].length;
				pivotAnim.Play("Anim_Superman");
				pivotAnim["Anim_Superman"].weight = 0.4f;
				Camera.main.GetComponent<CameraShake>().shakeDuration = 0.5f;

				for (int i = 0; i < boostEffect.Count; i++)
				{
					var emission = boostEffect[i].emission;
					emission.enabled = false;
				}
			}
		}

		private void PlayTurnAnim(string direction)
        {
			//pivotAnim["Anim_Rotate"+direction].layer = 1;
			pivotAnim.Play("Anim_Rotate" + direction);
			//pivotAnim["Anim_Rotate" + direction].weight = 0.4f;
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

		private void CenterPose()
        {
			if (pivotAnim.IsPlaying("Anim_RotateLeft"))
			{
				pivotAnim.Play("Anim_LeftToCenter");
			}
			else if (pivotAnim.IsPlaying("Anim_RotateRight"))
			{
				pivotAnim.Play("Anim_RightToCenter");
			}
		}
		

		public void ToggleProtect(bool state)
		{
			if (!state)
			{
				LevelManager.Instance.ActivateInvul(2f);
				Camera.main.GetComponent<CameraShake>().isShake = true;
				GameManager.Instance.LoseHelmets(1);
				helmetModel.GetComponent<CostumeScript>().PlayEffect();
				if (GameManager.Instance.CurrentHelmets < 0) { GameManager.Instance.CurrentHelmets = 0; }
				if (GameManager.Instance.CurrentHelmets < 1)
				{
					isProtect = state;
					helmetModel.SetActive(state);
				}
			}
			else
			{

				GameManager.Instance.AddHelmets(1);
				if (GameManager.Instance.CurrentHelmets > 3) { GameManager.Instance.CurrentHelmets = 3; }
				if (GameManager.Instance.CurrentHelmets > 0)
				{
					isProtect = state;
					helmetModel.SetActive(state);
				}
			}

			switch (GameManager.Instance.CurrentHelmets)
			{
				case 3:
					LevelManager.Instance.TemporarilyMultiplySpeed(2, 5f, "helmet");
					LevelManager.Instance.ActivateInvul(6f);
					PreSuperman();
					break;
				case 2:
					headContainer.transform.localScale = Vector3.one * 1.5f;
					break;
				case 1:
					headContainer.transform.localScale = Vector3.one;
					break;
				default:
					break;
			}
		}

		private void LookBackCheck()
        {
			if (GameManager.Instance.FuelPoints < 20 && !pivotAnim.isPlaying && !lookBack && !isSuperman)
			{
				//pivotAnim.Play("Anim_LookBack");
				pivotAnim["Anim_LookBack"].layer = 2;
				pivotAnim.Play("Anim_LookBack");
				pivotAnim["Anim_LookBack"].weight = 0.4f;
				lookBack = true;
			}
			else if (GameManager.Instance.FuelPoints > 20)
			{
				lookBack = false;
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
			Camera.main.GetComponent<CameraShake>().isShake = true;
			isDead = true;
			pivotAnim.Stop();
			GameManager.Instance.SlowMotion();
			Destroy(shadow);
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
			riderModel.SetActive(false);
			bikeModel.SetActive(false);
			ragdollObj.SetActive(true);
            riderRagdoll.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);
            bikeRagdoll.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

			foreach (Rigidbody rb in riderRagdoll.GetComponent<RagdollDeathScript>().ragdollBodies)
            {
                rb.AddExplosionForce(30f, new Vector3(transform.position.x + Random.Range(-0.7f, 0.7f), 0, -1f), 5f, 3f, ForceMode.Impulse);
            }

            Rigidbody bikeRb = bikeRagdoll.GetComponent<Rigidbody>();
            bikeRb.AddExplosionForce(25f, new Vector3(transform.position.x + Random.Range(-0.7f, 0.7f), -0.5f, -1f), 5f, 1f, ForceMode.Impulse);
            foreach (Collider collider in riderRagdoll.GetComponent<RagdollDeathScript>().ragdollColliders)
            {
                Physics.IgnoreCollision(bikeRagdoll.GetComponent<BoxCollider>(), collider, true);
				Physics.IgnoreCollision(bikeRagdoll.GetComponent<CapsuleCollider>(), collider, true);
			}
        }

		public void HurtPlayer(bool pushing, Transform hitLocation)
		{
			if(hitLocation != null)
				Instantiate(crashEffect, hitLocation.transform.position - (this.transform.position / 0.75f) + (Vector3.forward * 0.5f), crashEffect.transform.rotation);
			else
				Instantiate(crashEffect,this.transform.position + (Vector3.forward * 0.5f), crashEffect.transform.rotation);
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

		private void CheckAnimation()
        {
			if (!pivotAnim.IsPlaying("Anim_Slide") && isSlide)
			{
				isSlide = false;
				GetComponent<CapsuleCollider>().enabled = true;
				var emission = slideEffect.emission; //not really need this, disable in animation anyway
				emission.enabled = false;
			}

			if(!pivotAnim.IsPlaying("Bike_Wheelie") && isWheelie && !isSuperman)
            {
				isWheelie = false;
				isSpeed = false;
				var emission = boostEffect[3].emission;
				emission.enabled = false;
			}
		}

		private void StopWindFX()
        {
			var emission = boostEffect[3].emission;
			emission.enabled = false;
		}

		private void ResetStaticBool()
        {
			isDead = false;
			isInvul = false;
			isBoost = false;
			isSpeed = false;
			isProtect = false;
			isWheelie = false;
			isSuperman = false;
		}

		//DEBUG ZONE
		public void TurnFaster()
        {
			turnSpeedMultiply += 0.100f;
			GUIManager.Instance.TurnSpeedNumber();
        }
		public void TurnSlower()
		{
			turnSpeedMultiply -= 0.100f;
			GUIManager.Instance.TurnSpeedNumber();
		}

		public void OnOffAnimation()
        {
			animationOn = !animationOn;
        }
	}
}
