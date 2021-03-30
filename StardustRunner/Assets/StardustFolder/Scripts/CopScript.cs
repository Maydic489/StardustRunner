using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class CopScript : MonoBehaviour
	{
		private AudioSource runAudio;
		public float MoveSpeed = 5f;
		private Animator anim;
		[SerializeField]
		private bool isRunning = true;
		[SerializeField]
		private bool isRightState = true;

		private void Start()
		{
			var sM = SoundManager.Instance;
			anim = GetComponent<Animator>();
			runAudio = GetComponent<AudioSource>();
			if (sM.Settings.SfxOn)
			{
				SoundManager.Instance.ClearLoop();
				SoundManager.Instance.PlaySoundSource(runAudio, transform.position,true,true);
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (GameManager.Instance.FuelPoints > 25 /*|| GameManager.Instance.FuelPoints < 5*/)
			{
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(SimpleLane.playerPositoin.x, SimpleLane.playerPositoin.y,
					SimpleLane.playerPositoin.z - 1f - (GameManager.Instance.FuelPoints / 40)), MoveSpeed * Time.deltaTime); //change 40 for more or less distanc
			}
			else
			{
				//transform.position = Vector3.MoveTowards(transform.position, new Vector3(SimpleLane.playerPositoin.x, SimpleLane.playerPositoin.y,
				//   -1.21f), MoveSpeed * Time.deltaTime);
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(SimpleLane.playerPositoin.x, SimpleLane.playerPositoin.y,
					SimpleLane.playerPositoin.z - 1f - (GameManager.Instance.FuelPoints/40)), MoveSpeed * Time.deltaTime);
			}


			if (transform.position.z >= -1.05f && !anim.GetBool("isThere"))
			{
				anim.SetBool("isThere", true);
			}
			else
				anim.SetBool("isThere", false);

			if (!SimpleLane.isDead) //if player not dead, try to hit them
			{ 
				anim.SetBool("isHit", false);
				if(!anim.GetBool("isThere"))
					ActivateRunAudio(true);
			}
			else //if player is dead, stop hitting
			{
				ActivateRunAudio(false);
				anim.SetBool("isHit", true);
				anim.SetBool("isThere", false);
			}
		}

		private void ActivateRunAudio(bool state)
		{
			if (isRunning == anim.GetBool("isThere")) isRightState = false;
			else isRightState = true;

			if (!isRightState)
			{
				isRightState = true;
				isRunning = !anim.GetBool("isThere");
				if (!isRunning)
					SoundManager.Instance.StopLoopingSound(runAudio.clip.name);
				else
					SoundManager.Instance.PlaySoundSource(runAudio, transform.position, true, true);
			}
			else
				return;

			Debug.Log("change state");
		}
	}
}
