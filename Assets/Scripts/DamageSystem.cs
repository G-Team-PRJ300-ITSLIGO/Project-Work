using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour 
{
	
	public float damageAmount;
	private GameController GC;
	public int scoreValue;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			GC = gameControllerObject.GetComponent<GameController> ();
		}
		if (GC == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}

	}


	void Damage(GameObject objToTakeDMG)
	{
		objToTakeDMG.GetComponent<HealthSystem>().TakeDamage (objToTakeDMG, damageAmount);
		if(objToTakeDMG.tag == "Player")
		{
			CheckIfDead (objToTakeDMG);
		}
	}

	public void CheckIfDead(GameObject objToCheck)
	{
		if (objToCheck.GetComponent<HealthSystem>().health <= 0) 
		{
			GetComponent<DestroyOnCollision> ().PlayerKill (objToCheck);
		}
	}

	//Checks who needs to receive damage by comparing tags of two objects
	public void DetermineDamage(GameObject obj, GameObject other)
	{
		//Checks if Colliding object is player with enemy (Player will take damage)
		if (obj.tag == "Player" && other.tag == "Enemy") {
			Damage (obj);
		} 
		//Checks if Colliding object is enemy with player (Enemy will take damage)
		else if (obj.tag == "Enemy" && other.tag == "Player") {
			Damage (obj);
		} else if (obj.tag == "Enemy" && other.tag == "Enemy") {
			return;
		} else if (obj.tag == "Enemy" && other.tag == "PlayerWeapon")
		{
			Damage (obj);
			Destroy (other);
		}
		else 
		{
			GC.AddScore (scoreValue);
		}

		Destroy(gameObject);
	}
}
