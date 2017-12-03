using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	public float HP = 100;
	public float currentHP;
	public float Damage = 100;
	public int ScoreValue = 100;
	private scr_GameController gameController;

	void Start()
	{
		currentHP = HP;
		FindGC ();
	}

	void Update()
	{

		if (currentHP <= 0) {
			gameController.AddScore (ScoreValue);
			Destroy (gameObject);
		}
	}

	void FindGC()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<scr_GameController> ();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}
	}
}
