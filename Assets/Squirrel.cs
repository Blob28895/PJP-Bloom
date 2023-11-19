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
        target = GameObject.FindGameObjectWithTag("Target");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        //Debug.Log(transform.position.x + ":" + target.transform.position.x);
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
