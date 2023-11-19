using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Squirrel : MonoBehaviour
{
    private GameObject target;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Flower");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SearchForTarget()
    {
		
		GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        if (flowers.Length == 0) { return; }
        target = flowers[Random.Range(0, flowers.Length)];
        
    }

    void Update()
    {
        
        if (target == null) { 
            SearchForTarget(); 
        }
        //Debug.Log(transform.position.x + ":" + target.transform.position.x);
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if(target == null){ return;}

		if (transform.position.x < target.transform.position.x)
		{
			spriteRenderer.flipX = true;
		}
		else
		{
			spriteRenderer.flipX = false;
		}
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.003f);
	}
}
