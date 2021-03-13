using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class CopScript : MonoBehaviour
    {
        public AudioSource runAudio;
		public float MoveSpeed = 5f;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            ActivateRunAudio(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.FuelPoints > 25 /*|| GameManager.Instance.FuelPoints < 5*/)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(SimpleLane.playerPositoin.x, SimpleLane.playerPositoin.y,
                    SimpleLane.playerPositoin.z - 1f - (GameManager.Instance.FuelPoints / 10)), MoveSpeed * Time.deltaTime);
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

            if (!SimpleLane.isDead) 
            { 
                anim.SetBool("isHit", false);
                if(!anim.GetBool("isThere"))
                    ActivateRunAudio(true);
            }
            else
            {
                anim.SetBool("isHit", true);
                anim.SetBool("isThere", false);
                ActivateRunAudio(false);
            }
		}

        private void ActivateRunAudio(bool state)
        {
            if(runAudio.enabled != state)
                runAudio.enabled = state;
        }
	}
}
