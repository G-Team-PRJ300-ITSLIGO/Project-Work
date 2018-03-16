using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	//public Transform obj;
	private scr_GameController gameController;
	private scr_WaveSystem waves;
	private scr_playerBehaviour player;
	private float time;
	// Use this for initialization
	void Start () {
		FindGC();
	}
	
	// Update is called once per frame
	void Update () {

		if(time < 4f && time > -1f)
		time += Time.deltaTime;
            if (time >= 4f && gameController.player.locked)
            {
                gameController.enabled = true;
                gameController.player.locked = false;
                waves.active = true;
			time = -1f;
            }

			
	}

	void Awake()
	{
		if(gameController == null)
		{
			FindGC();
		}

	}

	void FixedUpdate()
	{
		if(gameController == null)
		{
			FindGC();
		}
	}


	void FindGC()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<scr_GameController> ();
			waves = gameControllerObject.GetComponent<scr_WaveSystem> ();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}
	}




}
