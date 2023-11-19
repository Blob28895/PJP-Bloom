using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
	[SerializeField] private Sprite bloomSprite;


	public void bloom()
	{
		GetComponent<SpriteRenderer>().sprite = bloomSprite;
	}
	
}
