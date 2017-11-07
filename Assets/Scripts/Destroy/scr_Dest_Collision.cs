using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Dest_Collision : MonoBehaviour {
	/// <summary>
	/// This script handles the collisions with different objects and depending on their 
	/// type/tag calls on different actions from different components.
	/// For example, if an object is a health pickup, it will destroy it and let a a different component handle the healing, in that case, Health Pickup Component.
	/// </summary>

	public GameObject explosion;
	public GameObject playerExplosion;
	private scr_GameController gameController;
	private scr_playerBehaviour player;


	// Use this for initialization
	void Start () {
		FindGC ();
	}

	//Decides how to dispose of an objectm depending on if it's a player or anything else.
	public void ObjectKill(GameObject obj)
	{
		if(obj.tag == "Player")
		{
			Instantiate (obj.GetComponent<scr_Dest_Collision> ().playerExplosion, obj.transform.position, obj.transform.rotation);
			gameController.GameOver ();
			Destroy (obj.gameObject);
			Destroy (gameObject);
		}
		else
		{
			Instantiate (obj.GetComponent<scr_Dest_Collision> ().explosion, obj.transform.position, obj.transform.rotation);
			obj.GetComponentInParent<scr_DamageSystem> ().addScore (obj.GetComponentInParent<scr_DamageSystem> ().scoreValue);
			Destroy (gameObject);
			Destroy (obj.gameObject);
		}

	}

	//Collision Detection Script
	void OnTriggerEnter(Collider other)
	{
		//Ignores the detection between boundary and enemies, pickups etc.
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PickupHealth") ) 
		{
			return;
		}
		//Ignores the detection between Enemy and its own bullets or weapons as well as other enemy with other enemy's weapons.
		if(gameObject.tag == "Enemy" && other.gameObject.tag == "EnemyWeapon" || gameObject.tag == "EnemyWeapon" && other.gameObject.tag == "Enemy"){
			return;
		}

		///All Damage Related stuff is checked here.
		//How these checks work:
		//First of all, the check is done if there is any damage system assigned at all. If not, informs in console.
		//Secondly, the game checks for assigned explosion sfx object, so if any explosion is required for the collision, if so, create it.
		//Next, the determine damage method is called to compare the two objects in it, decide who is going to take damage and deal the damage as well then check if object has any health left and do appropiate destroy action or game over if it's a player object.
		//Lastly adds score for player.

		//For Destroying enemies and damaging them.
		if (gameObject.GetComponentInParent<scr_DamageSystem> () != null) 
			{
				//Create exploision if assigned and needed.
				if (gameObject.GetComponent<scr_Dest_Collision> ().explosion != null) 
				{
					Instantiate (gameObject.GetComponent<scr_Dest_Collision> ().explosion, gameObject.transform.position, gameObject.transform.rotation);
				}
				gameObject.GetComponentInParent<scr_DamageSystem> ().DetermineDamage (other.gameObject, gameObject);

			}


		//Pickup System for restoring health.
		if (gameObject.tag == "PickupHealth" || other.tag == "PickupHealth") 
		{
			if (other.tag == "Player" || gameObject.tag == "Player") 
			{
				gameObject.GetComponentInParent<scr_HealingPickups>().RestoreHealth (other.gameObject);
				Destroy (gameObject);
			}       
			if (other.tag == "PlayerWeapon")
			{
				return;
			}
		}

		//Next two Ifs check if either object is player weapon or enemy's weapon and just destroy each other without any special FX.
		if(other.tag == "PlayerWeapon" || other.tag == "EnemyWeapon")
		{
			Destroy (other.gameObject);
		}
		if(gameObject.tag == "PlayerWeapon" || gameObject.tag == "EnemyWeapon"){
			Destroy (gameObject);
		}

		if(other.tag == "Player" && gameObject.tag == "Enemy"){
			Destroy (gameObject);
			gameObject.GetComponentInParent<scr_DamageSystem> ().addScore (gameObject.GetComponentInParent<scr_DamageSystem> ().scoreValue);
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
