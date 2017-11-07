using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DamageSystem : MonoBehaviour 
{
	//Amount of Damage that will be dealt by an object
	public float damageAmount;
	//Game Controlling Object - the main 'Director' of the game/level
	private scr_GameController GC;
	//Amount of score to be added.
	public int scoreValue;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			GC = gameControllerObject.GetComponent<scr_GameController> ();
		}
		if (GC == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}

	}


	//
	void Damage(GameObject objToTakeDMG)
	{
		if(objToTakeDMG.GetComponentInParent<scr_HealthSystem>() != null)
			objToTakeDMG.GetComponentInParent<scr_HealthSystem>().TakeDamage (objToTakeDMG, damageAmount);
		CheckIfDead (objToTakeDMG);
	}

	public void addScore(int score)
	{
		GC.AddScore (score);
	}

	public void CheckIfDead(GameObject objToCheck)
	{
		if(objToCheck.GetComponentInParent<scr_HealthSystem>() != null)
		if (objToCheck.GetComponentInParent<scr_HealthSystem>().health <= 0) 
		{
			GetComponentInParent<scr_Dest_Collision> ().ObjectKill (objToCheck);
		}
	}

	//Checks who needs to receive damage by comparing tags of two objects
	public void DetermineDamage(GameObject obj, GameObject other)
	{
		//Player takes damage from enemy
		if (obj.tag == "Player" && other.tag == "Enemy")
		{
			Damage (obj);
		} 
		//Player takes damage from enemy's weapon
		else if (obj.tag == "Player" && other.tag == "EnemyWeapon")
		{
			Damage (obj);
		}
		//If enemy collided with other enemy then do nothing
		else if (obj.tag == "Enemy" && other.tag == "Enemy") 
		{
			return;
		}
		//if Enemy gets hit by player's weapon
		else if (obj.tag == "Enemy" && other.tag == "PlayerWeapon")
		{
			Damage (obj);
		}
		else if (obj.tag == "PlayerWeapon" && other.tag == "Enemy")
		{
			Damage (other);
		}
		else 
		{
			GC.AddScore (scoreValue);
		}

	}
}
