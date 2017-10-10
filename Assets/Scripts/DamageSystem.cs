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
		if(objToTakeDMG.GetComponentInParent<HealthSystem>() != null)
		objToTakeDMG.GetComponent<HealthSystem>().TakeDamage (objToTakeDMG, damageAmount);
		CheckIfDead (objToTakeDMG);
	}

	public void addScore(int score)
	{
		GC.AddScore (score);
	}

	public void CheckIfDead(GameObject objToCheck)
	{
		if(objToCheck.GetComponentInParent<HealthSystem>() != null)
		if (objToCheck.GetComponent<HealthSystem>().health <= 0) 
		{
			GetComponent<DestroyOnCollision> ().ObjectKill (objToCheck);
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
