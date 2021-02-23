using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	public bool isShake { get; set; }

	// How long the object should shake for.
	public float shakeDuration = 0f;
	public float shakeDurationSet;
	private float saveShakeDuration;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}

		saveShakeDuration = shakeDuration;
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (isShake)
		{
			if(shakeDuration == 0) { shakeDuration = shakeDurationSet;}
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				camTransform.localPosition = originalPos;
				isShake = false;
			}
		}
	}

	public void ResetShakeDuration()
    {
		shakeDuration = saveShakeDuration;
    }
}