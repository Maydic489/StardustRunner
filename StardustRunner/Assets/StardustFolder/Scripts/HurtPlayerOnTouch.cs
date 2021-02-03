using System.Collections;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class HurtPlayerOnTouch : KillsPlayerOnTouch
	{
		public bool DontAutoCheckLane;
		public bool isGround;
		public char whatLane;

		private void Start()
		{
			if(!DontAutoCheckLane)
			Invoke("CheckLane", 1);
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
					if((SimpleLane.whatLane != this.whatLane) || this.whatLane == 'n')
					{
						LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().HurtPlayer(!isGround);				}
					else
						LevelManager.Instance.KillCharacter(player);
				}
				else
					LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<SimpleLane>().ToggleProtect(false);

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
