using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	private Collider2D[] overlap;
	private void Start()
	{
		overlap = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
		foreach (Collider2D col in overlap)
		{
			if(col.gameObject.CompareTag("Target"))
			{
				Destroy(col.gameObject);
			}
			if (col.CompareTag("Enemy"))
			{
				col.GetComponent<Squirrel>().Die();
			}
		}
	}

}
