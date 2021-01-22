using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class CopScript : MonoBehaviour
    {
		public float MoveSpeed = 5f;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(SimpleLane.playerPositoin.x, SimpleLane.playerPositoin.y,
				SimpleLane.playerPositoin.z - 1f - (GameManager.Instance.FuelPoints/10)), MoveSpeed * Time.deltaTime);

            if (transform.position.z == -0.9912834f)
                anim.SetBool("isThere", true);
		}
	}
}
