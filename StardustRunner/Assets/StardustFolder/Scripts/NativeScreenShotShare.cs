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

			shareMessage = "เรานี่แว้นเก่งจริงๆ เลย ได้คะแนนตั้ง "+GameManager.Instance.Points+"!";
			StartCoroutine(TakeScreenshotAndShare());
		}

		private IEnumerator TakeScreenshotAndShare()
		{
			yield return new WaitForEndOfFrame();

			Texture2D ss = new Texture2D(Screen.width*2, Screen.height, TextureFormat.RGB24, false);

			for (int y = 0; y < ss.height; y++) //set bg color before read screen pixel
			{
				for (int x = 0; x < ss.width*2; x++)
				{
					ss.SetPixel(x, y, Color.black);
				}
			}

			ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), (Screen.width/2), 0);
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
