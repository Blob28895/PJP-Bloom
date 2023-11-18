using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1f;
	[Header("Size Controls")]
	[SerializeField] public float maxScale = 4f;
	[SerializeField] public float minScale = 0.75f;
	[SerializeField] private float increaseRate = 0.25f;
	[SerializeField] private float decreaseRate = 0.1f;
	[SerializeField] private float movementThreshold = 0.15f;


    private SpriteRenderer _sprite;
	private Vector3 _baseScale;
	private Vector3 _lastMousePosition;


	void Start()
    {
		Cursor.visible = false;
        _sprite = GetComponent<SpriteRenderer>();
		_baseScale = transform.localScale;
		_lastMousePosition = transform.position;
	}


    void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;
        transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
    }

	private void FixedUpdate()
	{
		
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
		
		float currentScale = transform.localScale.x;
		//Debug.Log(currentScale);
		if (Vector3.Distance(_lastMousePosition, transform.position) > movementThreshold && currentScale > minScale) 
		{
			
			transform.localScale = new Vector3(currentScale - decreaseRate, currentScale - decreaseRate, 1f);
			//float speed = Math.Abs(Input.GetAxis("Mouse X")) + Math.Abs(Input.GetAxis("Mouse Y"));
			//Debug.Log("Speed: " + speed);
			//Debug.Log(Vector3.Distance(_lastMousePosition, transform.position));
		}
		else if (Vector3.Distance(_lastMousePosition, transform.position) < movementThreshold && currentScale < maxScale)
		{
			transform.localScale = new Vector3(currentScale + increaseRate, currentScale + increaseRate, 1f);
			//Debug.Log(Vector3.Distance(_lastMousePosition, transform.position));
		}
		_lastMousePosition = transform.position;
		/*float speedScale = Math.Max(0.1f, Math.Abs(Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y")));
		transform.localScale = new Vector3(speedScale * _baseScale.x, speedScale * _baseScale.x, 1f);*/
	}
}
