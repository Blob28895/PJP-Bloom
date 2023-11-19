using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
	public GameObject flowerBud;
	[Tooltip("Percentage of the width and height of the screen that flowers will be allowed to spawn")]
	[Range(0f, 1f)]
	public float screenCoverage;
	private BoxCollider2D _spawnArea;

	private void Start()
	{
		_spawnArea = GetComponent<BoxCollider2D>();


	}
	public void spawnFlowers(int amount)
	{

		float camHeight = Camera.main.orthographicSize * screenCoverage;
		float camWidth = camHeight * Camera.main.aspect * (screenCoverage + 0.1f);

		Vector3 spawnPosition = Vector3.one;
		for(int i = 0; i < amount; ++i)
		{
			spawnPosition.x = Random.Range(-1 * camWidth, camWidth);
			spawnPosition.y = Random.Range(-1 * camHeight, camHeight);

			Instantiate(flowerBud, spawnPosition, Quaternion.identity);
		}
	}

	private void Update()
	{
	}
}
