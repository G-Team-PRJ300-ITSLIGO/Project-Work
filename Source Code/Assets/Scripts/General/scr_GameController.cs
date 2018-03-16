using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class scr_GameController : MonoBehaviour {

	private scr_GameInstance instance;

    public float enemiesKilled;
    public float enemiesInStage;
    public float totalEnemiesKilled;

    public GameObject[] Pickups;
    public Image characterImage;
    public Animator HUDcharacter;
	//Amount of pickups on each wave
	public int pickupsCount;
	//Time in seconds before pickups are spawned
	public float spawnPickupWait;
	//Time in seconds before pickups start to appear
	public float startPickupWait;
	public Image healthBar;
    public Image chargeBar;
    public GameObject boundary;
    public GameObject HUDFade;

	public Text scoreText;
	public GameObject gameOverText;
	public Text healthText;
    public GameObject levelUI;
	private bool startGame = false;
	private bool isVisible = false;

	public Action<string,scr_playerBehaviour> powerupAction;
	public bool gameOver;
	public scr_PowerUpMethods methodList;
	public bool restart;
	private int score;
	public scr_playerBehaviour player;
	bool isInitialized = false;
    public bool bossKill = false;
    private bool paused;
    private bool transition;
    private float leaveSpeed;
	public Transform spawnPoint;
    public GameObject clearScreen;
    public Text[] gameClearTexts;
	//public GameObject TransitionCam;

	void Start(){
        HUDcharacter = characterImage.GetComponent<Animator>();



        if (!isInitialized)
		{
			GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

			if (gameInstancer != null) {
				instance = gameInstancer.GetComponent<scr_GameInstance> ();
			}

			instance.LoadUserProfile();
            GetComponent<scr_PowerUpMethods>().FindMethod(ref powerupAction, instance.UserProfile.playerEngine.CodeName, ref HUDcharacter);
            HUDcharacter.Play("idle");

            GameObject user = Instantiate (instance.ShipObjects.Find(obj => obj.name == instance.UserProfile.playerShip.ShipName),spawnPoint);
            Debug.Log("Thiis guy is" + user.tag);
			player = user.GetComponent<scr_playerBehaviour> ();
            ChangeStats();
            player.stats.difficulty = player.stats.Difficulty();
            powerupAction("Start", player);
            leaveSpeed = player.stats.speed/2;

			Initialize ();
			isInitialized = true;
		}
		enabled = false;


	}

	public void CarryStats()
	{
        instance.UserProfile.svStats = new SavableStats {
            Health = player.stats.currentHP,
            Damage = player.stats.Damage,
            FireRate = player.originalFireRate,
            Score = instance.UserProfile.playerStats.ScoreValue,
            Speed = player.stats.speed,
            Power = player.weaponStrength,
            specialPower = player.specialCollect
        };
        instance.SaveUserProfile();
	}

    public void ChangeStats()
    {
        player.stats.currentHP = instance.UserProfile.svStats.Health;
        player.stats.Damage = instance.UserProfile.svStats.Damage;
        player.stats.fireRate = instance.UserProfile.svStats.FireRate;
        if(instance.UserProfile.svStats.Power != 0)
        player.weaponStrength = instance.UserProfile.svStats.Power;
        player.stats.speed = instance.UserProfile.svStats.Speed;
        player.specialCollect = instance.UserProfile.svStats.specialPower;
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            instance.UserProfile.deaths++;
            instance.SaveUserProfile();
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
            enemiesInStage = GetComponent<scr_WaveSystem>().enemiesInStage;
        totalEnemiesKilled = enemiesKilled / enemiesInStage;

		if (gameOver)
		{
            gameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                instance.UserProfile.deaths++;
                instance.SaveUserProfile();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
		}
		if(bossKill)
        {
            if(!HUDFade.active)
            {
                HUDFade.SetActive(true);
            }

			instance.UpdateHighscoresFile(instance.UserProfile.ProfileName, SceneManager.GetActiveScene().name, instance.UserProfile.playerStats.ScoreValue);
			instance.UserProfile.CurrentLevelName = SceneManager.GetActiveScene ().name;
			instance.SaveUserProfile ();
            HUDcharacter.Play("bossWin");
            player.locked = true;
            player.GetComponent<Rigidbody>().velocity = transform.forward * leaveSpeed;
            leaveSpeed += 2 * Time.deltaTime;
            if(boundary.GetComponent<scr_Dest_Boundary>().playerExit)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
                transition = true;
                bossKill = false;
            }
            //levelUI.SetActive(true);
			//TransitionCam.SetActive (true);

        }

        if(transition)
        {
            player.locked = true;
            clearScreen.SetActive(true);
            Debug.Log(totalEnemiesKilled);
            gameClearTexts[0].text = "500";
            gameClearTexts[1].text = (Math.Round(totalEnemiesKilled , 2)*100).ToString() +"%";
            gameClearTexts[2].text = "x" + player.stats.difficulty.ToString();
            gameClearTexts[3].text = Math.Round((500 * player.stats.difficulty * totalEnemiesKilled),0).ToString();
            gameClearTexts[4].text = instance.UserProfile.playerStats.ScoreValue.ToString();
            instance.UserProfile.Crystals += (int)Math.Round((500 * player.stats.difficulty * totalEnemiesKilled), 0);
            CarryStats();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
            }
        }
		UpdateHealthText ();
	}



	//Method that randomly spawns the pickups inside player's play area.
	//IEnumerator SpawnPickups()
	//{
	//	yield return new WaitForSeconds (spawnPickupWait);
	//	while(true)
	//	{
	//		for (int i=0;i < pickupsCount; i++) 
	//		{
	//			GameObject pickup = Pickups [Random.Range (0, Pickups.Length)]; 
	//			Vector3 spawnPosition = new Vector3 (Random.Range (player.boundingArea.xMin, player.boundingArea.xMax), 0.0f , Random.Range (player.boundingArea.zMin, player.boundingArea.zMax));
	//			Quaternion spawnRotation = Quaternion.identity;
	//			Instantiate (pickup, spawnPosition, spawnRotation);
	//			yield return new WaitForSeconds (spawnPickupWait);
	//		}
	//	}
	//}

	//Anything that needs initializing of setting up before use to be put here
	void Initialize(){

		UpdateHealthText ();

		gameOver = false;
		score = 0;
		UpdateScore ();
        //TransitionCam.SetActive(false);


        //StartCoroutine (SpawnPickups ());
    }



	public void AddScore(int newScoreValue)
	{
		instance.UserProfile.playerStats.ScoreValue += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + instance.UserProfile.playerStats.ScoreValue;
	}

	void UpdateHealthText()
	{
        if (player != null)
            healthText.text = "Strength: " + player.weaponStrength;
		if(healthBar != null && player != null)
		{
			if (player.stats.currentHP > player.stats.HP) player.stats.currentHP =  player.stats.HP;
			healthBar.fillAmount = player.stats.currentHP /  player.stats.HP;
            chargeBar.fillAmount = player.powerUpMeter / 100;
		}

	}
	public void GameOver()
	{
        gameOver = true;
        Debug.Log("Game Over Man");
        
	}
}
