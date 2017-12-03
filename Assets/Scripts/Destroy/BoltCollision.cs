using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCollision : MonoBehaviour {
	/// <summary>
	/// This script handles the collisions with different objects and depending on their 
	/// type/tag calls on different actions from different components.
	/// For example, if an object is a health pickup, it will destroy it and let a a different component handle the healing, in that case, Health Pickup Component.
	/// </summary>

	public GameObject explosion;
	private scr_GameController gameController;
	private scr_playerBehaviour player;


	// Use this for initialization
	void Start () {
		FindGC ();
	}
		

	//Collision Detection Script
	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
		case "Enemy":
			other.GetComponent<Stats> ().currentHP -= this.GetComponent<Stats>().Damage;
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			break;
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
