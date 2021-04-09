using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class NativeScreenShotShare : MonoBehaviour
	{
		private string shareMessage;
		private int wholePoints;

        public void ClickShareButton()
        {
			wholePoints = (int)GameManager.Instance.Points;

			shareMessage = "https://play.google.com/store/apps/details?id=com.unity.trashdash";
			StartCoroutine(TakeScreenshotAndShare());
		}

		private IEnumerator TakeScreenshotAndShare()
		{
			yield return new WaitForEndOfFrame();

			Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
			ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			ss.Apply();

			string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
			File.WriteAllBytes(filePath, ss.EncodeToPNG());

			// To avoid memory leaks
			Destroy(ss);

			new NativeShare().AddFile(filePath)
				.SetSubject("Subject goes here").SetText(shareMessage)
				.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
				.Share();
		}
	}
}
