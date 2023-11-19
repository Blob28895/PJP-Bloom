using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelSpawner : MonoBehaviour
{
	[SerializeField] private GameObject squirrel;

	private BoxCollider2D _collider;

	private void Awake()
	{
		_collider = GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
    }

	public void spawnSquirrel(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			Instantiate(squirrel, new Vector3(Random.Range(-1 * _collider.bounds.extents.x / 2, _collider.bounds.extents.x / 2),
												Random.Range(-1 * _collider.bounds.extents.y / 2, _collider.bounds.extents.y / 2),
												1) + transform.position,
												Quaternion.identity,
												transform);
		}
	}
}
