using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("hit");
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("hit");
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		Debug.Log("hit");
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
		}
	}

}
