using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class InLaneSpawner : TimedSpawner
    {
		/// <summary>
		/// On update, we check if we should spawn or not
		/// </summary>
		protected override void Update()
		{
			if (OnlySpawnWhileGameInProgress && (GameManager.Instance.Status != GameManager.GameStatus.GameInProgress))
			{
				return;
			}

			if (_timeSinceLastSpawn > _timeUntilNextSpawn && IsInLane())
			{
				TimeSpawn();
			}

			_timeSinceLastSpawn += Time.deltaTime;
		}

		private bool IsInLane()
        {
			bool _isInLane = false;
			switch(transform.position.x)
            {
				case 1.6f:
					_isInLane = true;
					break;
				case 0f:
					_isInLane = true;
					break;
				case -1.6f:
					_isInLane = true;
					break;
				default:
					break;
			}
			return _isInLane;
        }
	}
}
