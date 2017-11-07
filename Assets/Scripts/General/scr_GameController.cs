using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_GameController : MonoBehaviour {

	public GameObject[] Pickups;
	//Amount of pickups on each wave
	public int pickupsCount;
	//Time in seconds before pickups are spawned
	public float spawnPickupWait;
	//Time in seconds before pickups start to appear
	public float startPickupWait;


	public GameObject[] hazards;
	//The values that will be used to determine where the enemies spawn
	public Vector3 spawnValues;
	//Amount of hazards in a wave
	public int hazardCount;
	//Time in seconds before pickups are spawned
	public float spawnWait;
	//Time in seconds before pickups start to appear
	public float startWait;
	//Time in Seconds that determines the amount of wait between waves of enemies.
	public float waveWait;
	  
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText healthText;

	private bool gameOver;
	private bool restart;
	private int score;
	scr_playerBehaviour player;
	bool isInitialized = false;

	void Start(){
		if(!isInitialized)
		{
			Initialize ();
			isInitialized = true;
		}

	}


	void Update(){
		if (restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
			}
		}

		foreach(scr_HealthSystem hs in GetComponents<scr_HealthSystem>())
		{
			hs.HealthBarUpdate ();
		}
		UpdateHealthText ();
	}



	//Method that randomly spawns the pickups inside player's play area.
	IEnumerator SpawnPickups()
	{
		yield return new WaitForSeconds (spawnPickupWait);
		while(true)
		{
			for (int i=0;i < pickupsCount; i++) 
			{
				GameObject pickup = Pickups [Random.Range (0, Pickups.Length)]; 
				Vector3 spawnPosition = new Vector3 (Random.Range (player.boundingArea.xMin, player.boundingArea.xMax), spawnValues.y, Random.Range (player.boundingArea.zMin, player.boundingArea.zMax));
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (pickup, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnPickupWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	//Anything that needs initializing of setting up before use to be put here
	void Initialize(){
		GameObject playerControllerObj = GameObject.FindWithTag ("Player");
		if (playerControllerObj != null) 
		{
			player = playerControllerObj.GetComponent<scr_playerBehaviour> ();
		}
		if (player == null)
		{
			Debug.Log ("Cannot Find 'PlayerController' Script");
		}

		UpdateHealthText ();

		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
		StartCoroutine (SpawnPickups ());
		}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i=0;i < hazardCount; i++) 
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)]; 
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	void UpdateHealthText()
	{
		if(player != null)
			healthText.text = "Health: " + player.GetComponent<scr_HealthSystem>().health;
	}
	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
