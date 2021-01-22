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
		public GameObject bikeModel;
		public float MoveSpeed = 5f;
		public GameObject mainCamera;
		public static Vector3 playerPositoin;
		private float slideDirection;
		private char whatLane;

        protected override void Start()
        {
			mainCamera = GameObject.Find("Main Camera");
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

			if(transform.position.x > -1f && transform.position.x < 1f)
            {
				whatLane = 'm';
            }
			else if(transform.position.x < 0)
            {
				whatLane = 'l';
            }
			else if(transform.position.x > 0)
            {
				whatLane = 'r';
            }

			if(transform.position.x != slideDirection)
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), MoveSpeed * Time.deltaTime);
			mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(slideDirection*0.8f, mainCamera.transform.position.y, mainCamera.transform.position.z), (MoveSpeed*0.8f) * Time.deltaTime);

			playerPositoin = transform.position;
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

		protected override void CheckDeathConditions()
		{
			if (LevelManager.Instance.CheckDeathCondition(GetPlayableCharacterBounds()) || GameManager.Instance.FuelPoints <= 0f)
			{
				LevelManager.Instance.KillCharacter(this);
			}
		}

		public override void Die()
		{
			Destroy(bikeModel);
			riderModel.GetComponent<RagdollDeathScript>().ToggleRagdoll(true);

			foreach(Rigidbody rb in riderModel.GetComponent<RagdollDeathScript>().ragdollBodies)
            {
				rb.AddExplosionForce(3f, new Vector3(transform.position.x, 0, -1f), 3f, 2f,ForceMode.Impulse);
            }
		}
	}
}
