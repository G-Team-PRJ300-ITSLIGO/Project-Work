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


	public void PlayerKill(GameObject player)
	{
		if(playerExplosion != null)
		{
			Instantiate (playerExplosion, player.transform.position, player.transform.rotation);
			gameController.GameOver ();
			Destroy (player.gameObject);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PickupHealth")) 
		{
			return;
		}

		if (explosion != null) 
		{
			if(gameObject.GetComponentInParent<DamageSystem> () != null){
				Instantiate (explosion, transform.position, transform.rotation);
				gameObject.GetComponentInParent<DamageSystem> ().DetermineDamage (other.gameObject, gameObject);
				return;
			}
		}

		if (gameObject.tag == "PickupHealth") 
		{
			if (other.tag == "Player") 
			{
				if(other.gameObject.GetComponentInParent<HealthPickup>() != null)
				{
					other.gameObject.GetComponentInParent<HealthPickup>().RestoreHealth (other.gameObject);
				}
			}       
			if (other.tag == "PlayerWeapon")
			{
				return;
			} 

		}
		Destroy(gameObject);
	}
}
