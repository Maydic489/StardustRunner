﻿using UnityEngine;
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
		public GameObject riderRagdoll;
		public GameObject bikeRagdoll;
		public GameObject ragdollObj;
		public GameObject shadow;
		public GameObject groundPivot;
		private Animation pivotAnim;
		public GameObject helmetModel;
		public GameObject headContainer;
		public float MoveSpeed = 5f;
		private float slowSpeed = 0.1f;
		public GameObject mainCamera;
		public static Vector3 playerPositoin;
		public float slideDirection;
		private float oldDirection;
		public static char whatLane;
		public char inLane;
		public static bool isDead;
		public static bool isInvul;
		public static bool isProtect;
		public int protectLayer;
		private bool isSlide;
		private bool isWheelie;
		private bool lookBack;
		public static bool isSpeed {get; set;}
		private bool isSuperman;
		static int s_BlinkingValueHash;

		protected override void Start()
        {
			mainCamera = GameObject.Find("Main Camera");
			s_BlinkingValueHash = Shader.PropertyToID("_BlinkingValue");
			pivotAnim = groundPivot.GetComponent<Animation>();
			Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);
			isDead = false;
			isInvul = true;
			isSpeed = false;
			isProtect = false;
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
			CheckSpeedBoost();

			//old way to move, not good for rigidbody
			//if(transform.position.x != slideDirection)
			//transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.deltaTime);

			//if (groundPivot.GetComponent<Rigidbody>().transform.rotation.z != (slideDirection * 10))
			//{
			//    groundPivot.GetComponent<Rigidbody>().transform.Rotate(new Vector3(0, 0, (-100 * slideDirection) * Time.deltaTime));
			//}

			

			if(GameManager.Instance.FuelPoints < 40 && !pivotAnim.isPlaying && !lookBack && !isSuperman)
            {
				//pivotAnim.Play("Anim_LookBack");
				pivotAnim["Anim_LookBack"].layer = 2;
				pivotAnim.Play("Anim_LookBack");
				pivotAnim["Anim_LookBack"].weight = 0.4f;
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
                if (slowSpeed < MoveSpeed)
                {
                    slowSpeed += slowSpeed * (0.05f + slowSpeed);
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), slowSpeed * Time.deltaTime);
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

		public override void LeftStart()
		{
			oldDirection = slideDirection;
            //_rigidbodyInterface.AddForce(Vector3.left * MoveSpeed * Time.deltaTime);
            if (whatLane == 'r' && slideDirection != 0f)
            {
                slideDirection = 0f;
				if (!isSlide && !isWheelie)
					PlayTurnAnim("Left");
            }
            else
            {
                slideDirection = -1.6f;
				if (!isSlide && !isWheelie && whatLane != 'l')
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
				if (!isSlide && !isWheelie)
					PlayTurnAnim("Right");
			}
			else
			{
				slideDirection = 1.6f;
				if (!isSlide && !isWheelie && whatLane != 'r')
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
			}
		}

        public override void UpStart()
        {
			if (!isSuperman && !isSlide && !isWheelie)
			{
				isWheelie = true;
                pivotAnim["Bike_Wheelie"].layer = 1;
                pivotAnim.Play("Bike_Wheelie");
                pivotAnim["Bike_Wheelie"].weight = 0.4f;
				LevelManager.Instance.TemporarilyMultiplySpeed(2f, 0.5f);
			}
		}

		public void CheckSpeedBoost()
        {
			if (isSpeed && !isSuperman && !isWheelie) { PlaySupermanAnim(isSpeed); }
			else if (!isSpeed && isSuperman) { PlaySupermanAnim(isSpeed); }
		}

        public void PlaySupermanAnim(bool state)
        {
			if (state)
			{
				isSuperman = true;
				pivotAnim["Anim_Superman"].layer = 1;
				pivotAnim.Play("Anim_Superman");
				pivotAnim["Anim_Superman"].weight = 0.4f;
			}
			else
            {
				isSuperman = false;
				pivotAnim["Anim_Superman"].layer = 1;
				pivotAnim["Anim_Superman"].speed = -1;
				pivotAnim["Anim_Superman"].time = pivotAnim["Anim_Superman"].length;
				pivotAnim.Play("Anim_Superman");
				pivotAnim["Anim_Superman"].weight = 0.4f;
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
			if (!state)
			{
				LevelManager.Instance.ActivateInvul(2f);
				Camera.main.GetComponent<CameraShake>().isShake = true;
				protectLayer--;
				helmetModel.GetComponent<CostumeScript>().PlayEffect();
				if (protectLayer < 0) { protectLayer = 0; }
				if (protectLayer < 1)
				{
					isProtect = state;
					helmetModel.SetActive(state);
				}
			}
			else
			{

				protectLayer++;
				if (protectLayer > 3) { protectLayer = 3; }
				if (protectLayer > 0)
				{
					isProtect = state;
					helmetModel.SetActive(state);
				}
			}

			switch (protectLayer)
			{
				case 3:
					LevelManager.Instance.TemporarilyMultiplySpeed(2, 5);
					LevelManager.Instance.ActivateInvul(6);
					protectLayer = 2;
					break;
				case 2:
					headContainer.transform.localScale = Vector3.one * 1.5f;
					break;
				case 1:
					headContainer.transform.localScale = Vector3.one;
					break;
				default:
					print("Out of Helmet");
					break;
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

		private void CheckAnimation()
        {
			if (!pivotAnim.IsPlaying("Anim_Slide") && isSlide)
			{
				isSlide = false;
				GetComponent<CapsuleCollider>().enabled = true;
			}

			if(!pivotAnim.IsPlaying("Bike_Wheelie") && isWheelie)
            {
				isWheelie = false;
				isSpeed = false;
			}
		}
	}
}
