using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class Tutorial : MonoBehaviour
	{
		public GameObject tutorialPanel;
		private TextMeshProUGUI tutorialPanelText;
		[SerializeField]
		private string tutorialType;
		private Text tutorialText;
		private bool isStop;

		private void Start()
		{
			tutorialPanelText = tutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
			tutorialText = GetComponent<Text>();
		}

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
				tutorialPanelText.text = tutorialText.text;
				tutorialPanel.SetActive(true);
				isStop = true;
			}
		}

		private void Update()
		{
			if(tutorialPanel.activeSelf && Time.timeScale != 0)
			{
				tutorialPanel.SetActive(false);
			}
		}
	}
}
