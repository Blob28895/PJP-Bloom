using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] squirrelSpawners;
	[SerializeField] private FlowerSpawner flowerSpawner;

	[Header("Menu Controls")]
	[SerializeField] private GameObject titleScreen;
	[SerializeField] private GameObject aimingScreen;
	[SerializeField] private GameObject aimingTargets;
	[SerializeField] private GameObject goalScreen;
	[SerializeField] private GameObject goalTargets;

	[Header("First round values")]
	[SerializeField] private int squirrelCount;
	[SerializeField] private int flowerCount;

	[Header("Round Controls")]
	[SerializeField] private int squirrelIncrease;
	[SerializeField] private int flowerIncrease;
	[SerializeField] private float spawnRate;
	[SerializeField] private float spawnRateIncrease;


	private int round = 0;
	private bool targetCheck = false;
	private bool allSquirrelsSpawned = false;
	public void startRound()
	{
		allSquirrelsSpawned = false;
		flowerSpawner.spawnFlowers(flowerCount + round * flowerIncrease);
		//spawnSquirrelsatRandomSpawners(squirrelCount + round * squirrelIncrease);
		StartCoroutine(spawnSquirrelsforRound(squirrelCount + round * squirrelIncrease));
		round++;
	}

	/*private void spawnSquirrelsatRandomSpawners(int amount)
	{
		for(int i = 0; i < amount; ++i)
		{
			squirrelSpawners[Random.Range(0, squirrelSpawners.Length - 1)].GetComponent<SquirrelSpawner>().spawnSquirrel(1);
		}
		allSquirrelsSpawned = true;
	}*/

	private IEnumerator spawnSquirrelsforRound(int amount)
	{
		for (int i = 0; i < amount; ++i)
		{
			squirrelSpawners[Random.Range(0, squirrelSpawners.Length - 1)].GetComponent<SquirrelSpawner>().spawnSquirrel(1);
			yield return new WaitForSeconds(spawnRate);
		}
		allSquirrelsSpawned = true;
	}

	private void Start()
	{
		//startRound();
	}
	private void Update()
	{
		if (targetCheck)
		{
			if(GameObject.FindGameObjectsWithTag("Target").Length == 0)
			{
				if(aimingTargets.activeInHierarchy)
				{
					targetCheck = false;
					aimingTargets.SetActive(false);
					aimingScreen.SetActive(false);
					goalScreen.SetActive(true);
					goalTargets.SetActive(true);
					targetCheck = true;
				}
				else if(goalScreen.activeInHierarchy)
				{
					goalScreen.SetActive(false);
					targetCheck = false;
					startRound();
				}
			}
		}
	}

	private bool SquirrelsAlive()
	{
		GameObject[] squirrels = GameObject.FindGameObjectsWithTag("Enemy");
		if (squirrels.Length == 0)
		{
			return false;
		}
		return true;
	}

	public void startButtonPressed()
	{
		titleScreen.SetActive(false);
		aimingScreen.SetActive(true);
		aimingTargets.SetActive(true);
		targetCheck = true;
	}

	
}
