using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class CoinSpawner : DistanceSpawner
	{
		public float setGap; //space between a set of coins
		public float setSizeMax; //how many coin in a set
		public float spawnedCoints; //count how many coin we spawn in one set
		public bool isSpawnPause; //determine if we should pause spawning

		/// <summary>
		/// Checks if the conditions for a new spawn are met, and if so, triggers the spawn of a new object
		/// </summary>
		protected override void CheckSpawn()
		{
			// if we've set our distance spawner to only spawn when the game's in progress :
			if (OnlySpawnWhileGameInProgress)
			{
				if ((GameManager.Instance.Status != GameManager.GameStatus.GameInProgress) && (GameManager.Instance.Status != GameManager.GameStatus.Paused))
				{
					_lastSpawnedTransform = null;
					return;
				}
			}

			// if we haven't spawned anything yet, or if the last spawned transform is inactive, we reset to first spawn.
			//but first check if it should pause between sets.
			if (isSpawnPause && _lastSpawnedTransform != null)
			{
				if (transform.InverseTransformPoint(_lastSpawnedTransform.position).x < -_nextSpawnDistance - setGap)
				{
					isSpawnPause = false;
					_lastSpawnedTransform = null; //null so it start spawning at the spawner instead of the last coin position
					return;
				}

			}
			else if (_lastSpawnedTransform == null)
			{
				DistanceSpawn(transform.position + MMMaths.RandomVector3(MinimumGap, MaximumGap));
				return;
			}
			else if (_lastSpawnedTransform != null)
			{
				if (!_lastSpawnedTransform.gameObject.activeInHierarchy)
				{
					DistanceSpawn(transform.position + MMMaths.RandomVector3(MinimumGap, MaximumGap));
					return;
				}
			}

			// if the last spawned object is far ahead enough, we spawn a new object
			if(isSpawnPause)
			{
				if (transform.InverseTransformPoint(_lastSpawnedTransform.position).x < -_nextSpawnDistance-setGap)
				{
					isSpawnPause = false;
				}

			}
			else if (transform.InverseTransformPoint(_lastSpawnedTransform.position).x < -_nextSpawnDistance)
			{
				Vector3 spawnPosition = transform.position;
				spawnedCoints++;
				DistanceSpawn(spawnPosition);
			}

			if(spawnedCoints >= setSizeMax)
			{
				isSpawnPause = true;
				spawnedCoints = 0;
			}
		}

		/// <summary>
		/// Spawns an object at the specified position and determines the next spawn position
		/// </summary>
		/// <param name="spawnPosition">Spawn position.</param>
		protected override void DistanceSpawn(Vector3 spawnPosition)
		{
			// we spawn a gameobject at the location we've determined previously
			GameObject spawnedObject = Spawn(spawnPosition, false);

			// if the spawned object is null, we're gonna start again with a fresh spawn next time we get fresh objects.
			if (spawnedObject == null)
			{
				_lastSpawnedTransform = null;
				_nextSpawnDistance = UnityEngine.Random.Range(MinimumGap.x, MaximumGap.x);
				return;
			}

			// we need to have a poolableObject component for the distance spawner to work.
			if (spawnedObject.GetComponent<MMPoolableObject>() == null)
			{
				throw new Exception(gameObject.name + " is trying to spawn objects that don't have a PoolableObject component.");
			}

			// if we have a movingObject component, we rotate it towards movement if needed
			if (SpawnRotatedToDirection)
			{
				spawnedObject.transform.rotation *= transform.rotation;
			}
			// if this is a moving object, we tell it to move in the designated direction
			if (spawnedObject.GetComponent<MovingObject>() != null)
			{
				spawnedObject.GetComponent<MovingObject>().SetDirection(transform.rotation * Vector3.left);
			}

			// if we've already spawned at least one object, we'll reposition our new object according to that previous one
			if (_lastSpawnedTransform != null)
			{
				// we center our object on the spawner's position
				spawnedObject.transform.position = transform.position;

				// we determine the relative x distance between our spawner and the object.
				float xDistanceToLastSpawnedObject = transform.InverseTransformPoint(_lastSpawnedTransform.position).x;

				// we position the new object so that it's side by side with the previous one,
				// taking into account the width of the new object and the last one.
				spawnedObject.transform.position += transform.rotation
													* Vector3.right
													* (xDistanceToLastSpawnedObject
													+ _lastSpawnedTransform.GetComponent<MMPoolableObject>().Size.x / 2
													+ spawnedObject.GetComponent<MMPoolableObject>().Size.x / 2);

				// if gaps are relative to the spawner
				if (GapOrigin == GapOrigins.Spawner)
				{
					spawnedObject.transform.position += (transform.rotation * ClampedPosition(MMMaths.RandomVector3(MinimumGap, MaximumGap)));
				}
				else
				{
					//MMDebug.DebugLogTime("relative y pos : "+spawnedObject.transform.InverseTransformPoint(_lastSpawnedTransform.position).y);

					_gap.x = UnityEngine.Random.Range(MinimumGap.x, MaximumGap.x);
					_gap.y = spawnedObject.transform.InverseTransformPoint(_lastSpawnedTransform.position).y + UnityEngine.Random.Range(MinimumGap.y, MaximumGap.y);
					_gap.z = spawnedObject.transform.InverseTransformPoint(_lastSpawnedTransform.position).z + UnityEngine.Random.Range(MinimumGap.z, MaximumGap.z);

					spawnedObject.transform.Translate(_gap);

					spawnedObject.transform.position = (transform.rotation * ClampedPositionRelative(spawnedObject.transform.position, transform.position));
				}
			}
			else
			{
				// we center our object on the spawner's position
				spawnedObject.transform.position = transform.position;
				// if gaps are relative to the spawner
				spawnedObject.transform.position += (transform.rotation * ClampedPosition(MMMaths.RandomVector3(MinimumGap, MaximumGap)));
			}

			// if what we spawn is a moving object (it should usually be), we tell it to move to account for initial movement gap
			if (spawnedObject.GetComponent<MovingObject>() != null)
			{
				spawnedObject.GetComponent<MovingObject>().Move();
			}

			//we tell our object it's now completely spawned
			spawnedObject.GetComponent<MMPoolableObject>().TriggerOnSpawnComplete();
			foreach (Transform child in spawnedObject.transform)
			{
				if (child.gameObject.GetComponent<ReactivateOnSpawn>() != null)
				{
					child.gameObject.GetComponent<ReactivateOnSpawn>().Reactivate();
				}
			}

			// we determine after what distance we should try spawning our next object
				_nextSpawnDistance = spawnedObject.GetComponent<MMPoolableObject>().Size.x / 2;
			// we store our new object, which will now be the previously spawned object for our next spawn
			_lastSpawnedTransform = spawnedObject.transform;

		}
	}
}
