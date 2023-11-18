using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	private float shakeDuration = 0f;
	[SerializeField] private float shakeMagnitude = 0.7f;
	private float dampingSpeed = 1f;
	Vector3 initialPosition;


	public void shakeCamera(float duration) {
		shakeDuration = duration;
	}
	private void OnEnable()
	{
		initialPosition = transform.localPosition;
	}

    void Update()
    {
		if (shakeDuration > 0)
		{
			transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			transform.localPosition = initialPosition;
		}
	}
}
