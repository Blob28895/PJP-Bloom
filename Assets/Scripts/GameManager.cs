using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] squirrelSpawners;
	[SerializeField] private FlowerSpawner flowerSpawner;

	[Header("UI Controls")]
	[SerializeField] private GameObject titleScreen;
	[SerializeField] private GameObject aimingScreen;
	[SerializeField] private GameObject aimingTargets;
	[SerializeField] private GameObject goalScreen;
	[SerializeField] private GameObject goalTargets;
	[SerializeField] private GameObject roundText;
	[SerializeField] private GameObject scoreText;

	[Header("First round values")]
	[SerializeField] private int squirrelCount;
	[SerializeField] private int flowerCount;

	[Header("Round Controls")]
	[SerializeField] private int squirrelIncrease;
	[SerializeField] private int flowerIncrease;
	[SerializeField] private float spawnRate;
	[SerializeField] private float spawnRateIncrease;
	[SerializeField] private float movementSpeedIncrease = 0.05f;


	private int round = 0;
	private bool targetCheck = false;
	private bool allSquirrelsSpawned = false;
	private bool inRound = false;
	private int flowersHeldBySquirrels = 0;
	private int score = 0;
	public IEnumerator startRound()
	{
		allSquirrelsSpawned = false;

		//Show Round count
		roundText.SetActive(true);
		roundText.GetComponent<TextMeshProUGUI>().text = "Round: " + (round + 1).ToString();
		yield return new WaitForSeconds(1.5f);
		roundText.SetActive(false);

		//Spawn Flowers
		for(int i = 0; i < flowerCount + round * flowerIncrease; ++i)
		{
			flowerSpawner.spawnFlowers(1);
			yield return new WaitForSeconds(0.25f);
		}
		yield return new WaitForSeconds(1);

		Debug.Log("multiplier: " + movementSpeedIncrease * round);
		//Spawn Squirrels
		for (int i = 0; i < squirrelCount + round * squirrelIncrease; ++i)
		{
			squirrelSpawners[Random.Range(0, squirrelSpawners.Length)].GetComponent<SquirrelSpawner>().spawnSquirrel(1, movementSpeedIncrease * round);
			yield return new WaitForSeconds(Mathf.Max(spawnRate - (spawnRateIncrease * round), 0.15f));
		}
		allSquirrelsSpawned = true;

		round++;
		inRound = true;
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
					StartCoroutine(startRound());
				}
			}
		}
		if(inRound)
		{
			if(!flowersAlive() || (!SquirrelsAlive() && allSquirrelsSpawned))
			{
				StartCoroutine(endRound());
			}
		}
	}

	private IEnumerator endRound()
	{
		inRound = false;
		yield return new WaitForSeconds(1);
		GameObject[] remainingFlowers = GameObject.FindGameObjectsWithTag("Flower");
		foreach(GameObject flower in remainingFlowers)
		{
			flower.GetComponent<Flower>().bloom();
			addScore(100);
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(1f);
		foreach(GameObject flower in remainingFlowers)
		{
			Destroy(flower);
		}
		allSquirrelsSpawned = false;
		StartCoroutine(startRound());
	}

	private bool flowersAlive()
	{
		GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
		if (flowers.Length == 0 || flowersHeldBySquirrels > 0)
		{
			return true;
		}
		return true;
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

	public void updateSquirrelFlowerCount(int i)
	{
		flowerCount += i;
	}
	public void addScore(int i)
	{
		score += i;
		scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString(); 
	}
}
