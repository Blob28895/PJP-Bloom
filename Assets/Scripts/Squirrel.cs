using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Squirrel : MonoBehaviour
{
    [SerializeField] private GameObject flower;
    [SerializeField] private float movementSpeed = 0.003f;

    private Vector2 escapeDirection = Vector2.one;
    private bool carryingFlower = false;
    private GameObject target;
    private Vector3 randomTargetPos;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Flower");
        //SearchForTarget();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SearchForTarget()
    {
		
		GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        if(flowers.Length == 0)
        {
            moveTowardsRandomPos();
            return;
        }
        target = flowers[Random.Range(0, flowers.Length)];
        
    }

    void FixedUpdate()
    {
		if (carryingFlower) { 
            Escape(); 
            return; 
        }
		if (target == null && !carryingFlower) { 
            SearchForTarget(); 
			return;
        }
        MoveTowardsTarget();

    }
    
    private void moveTowardsRandomPos()
    {
        if(randomTargetPos == null || Vector3.Distance(transform.position, randomTargetPos) < 0.001f)
        {
            randomTargetPos = new Vector3(Random.Range(Camera.main.orthographicSize * Camera.main.aspect / 2 * -1, Camera.main.orthographicSize * Camera.main.aspect / 2), 
                Random.Range(Camera.main.orthographicSize  * -1 / 2, Camera.main.orthographicSize / 2),
                1);
            //Debug.Log(randomTargetPos);
        }
        if(randomTargetPos.x > transform.position.x)
        {
			flip(-1);
		}
        else
        {
            flip(1);
        }
		transform.position = Vector3.MoveTowards(transform.position, randomTargetPos, movementSpeed);
	}
	void MoveTowardsTarget()
    {
		//Debug.Log("LSLSL");
		if (target == null || GameObject.FindGameObjectsWithTag("Flower").Length == 0)
        {
            
            moveTowardsRandomPos();  
            return;
        }

		if (transform.position.x < target.transform.position.x)
		{
			flip(-1);
		}
		else
		{
			flip(1);
		}
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed);
	}

    void Escape()
    {
        transform.position = Vector3.MoveTowards(transform.position, escapeDirection, movementSpeed);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Flower") && !carryingFlower) 
        {
            Destroy(collision.gameObject);
            carryingFlower = true;
            target = null;
            escapeDirection = Random.insideUnitCircle.normalized * 100;
            if(escapeDirection.x > 0)
            {
                flip(-1);
			}
            else
            {
                flip(1);
			}
            return;
		}

        if (collision.CompareTag("Border"))
        {
            Debug.Log("Squirrel Escaped with a flower!");
            Destroy(gameObject);
        }
    }

    private void flip(int value)
    {
		Vector3 newScale = transform.localScale;
		newScale.x = value;
		transform.localScale = newScale;
	}

    public void Die()
    {
        if (carryingFlower)
        {
            carryingFlower = false;
            Instantiate(flower, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
