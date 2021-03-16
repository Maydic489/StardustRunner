﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class Tutorial : MonoBehaviour
	{
		public GameObject tutorialText;
		[SerializeField]
		private string tutorialType;
		private bool isStop;

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
				TriggerEnter(other.gameObject);
		}

		private void TriggerEnter(GameObject collidingObject)
		{
			if (!isStop)
			{
				GameManager.Instance.PauseGeneric();
				TutorialTouch.Instance.ChangeTutorialType(tutorialType);
				isStop = true;
			}
		}
	}
}
