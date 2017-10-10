using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCollision : MonoBehaviour 
{
	/// <summary>
	/// This script handles the collisions with different objects and depending on their 
	/// type/tag calls on different actions from different components.
	/// For example, if an object is a health pickup, it will destroy it and let a a different component handle the healing, in that case, Health Pickup Component.
	/// </summary>

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	private PlayerController player;


	void Start()
	{

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}
	}

	//This method takes in an object (which is a player) and creates an explosion effect at his transformation position, instantiates a game over method and destroys the player and object it colided with.
	public void ObjectKill(GameObject obj)
	{
		//###FIX NOTE### 10.10.17 ###Fixed player not dying by asteroids.
		//Fixed by Changing first parameter in Instantiate from "playerExplosion" to get the playerExplosion that's directly assigned to player object

		//###FIX NOTE### 10.10.17 ###Fixed Enemies not dying or loosing health.
		//Fixed by creating a seperate check to see if object to be killed is a player, if it is, do a game over, if it's enemy, just kill it.
		if(obj.tag == "Player")
		{
			Instantiate (obj.GetComponent<DestroyOnCollision>().playerExplosion, obj.transform.position, obj.transform.rotation);
			gameController.GameOver ();
			Destroy (obj.gameObject);
			Destroy (gameObject);
		}
		else
		{
			Instantiate (obj.GetComponent<DestroyOnCollision>().playerExplosion, obj.transform.position, obj.transform.rotation);
			obj.GetComponentInParent<DamageSystem> ().addScore (obj.GetComponentInParent<DamageSystem> ().scoreValue);
			Destroy (obj.gameObject);
			Destroy (gameObject);
		}

	}

	//Collision Detection Script
	void OnTriggerEnter(Collider other)
	{
		//Ignores the detection between boundary and enemies, pickups etc.
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PickupHealth")) 
		{
			return;
		}

		//For Destroying enemies and damaging them.
		if (explosion != null) 
		{
			if(gameObject.GetComponentInParent<DamageSystem> () != null){
				Instantiate (explosion, transform.position, transform.rotation);
				gameObject.GetComponentInParent<DamageSystem> ().DetermineDamage (other.gameObject, gameObject);
			}
		}

		//Pickup System for restoring health.
		if (gameObject.tag == "PickupHealth" || other.tag == "PickupHealth") 
		{
			if (other.tag == "Player" || gameObject.tag == "Player") 
			{
				//###FIX NOTE### 10.10.17
				//Health Pickups fixed
				//The problem was that I accidentally had the wrong object asked to .GetComponent<HealthPickup>
				//Basically I was asking the player (who is not a health pickup and doesn't have that component) for Health Pickup Component.
				//That threw a null pointer for the obvious reason stated above.
				gameObject.GetComponentInParent<HealthPickup>().RestoreHealth (other.gameObject);
				Destroy (gameObject);
			}       
			if (other.tag == "PlayerWeapon")
			{
				return;
			}
		}

		if(other.tag == "PlayerWeapon" || other.tag == "EnemyWeapon")
		{
			Destroy (other.gameObject);
		}
		if(gameObject.tag == "PlayerWeapon" || gameObject.tag == "EnemyWeapon"){
			Destroy (gameObject);
		}

		if(other.tag == "Player" && gameObject.tag == "Enemy"){
			Destroy (gameObject);
			gameObject.GetComponentInParent<DamageSystem> ().addScore (gameObject.GetComponentInParent<DamageSystem> ().scoreValue);
		}

	}
}
