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
    public GameObject healthPowerup;
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

			if(obj.GetComponentInParent<scr_HealthSystem>().health <= 0){
				//Debug.Log (string.Format ("{0} health: {1}", obj.name, obj.GetComponentInParent<scr_HealthSystem> ().health));
				Destroy (obj.gameObject);
			}
			else{
				return;
			}
		}

	}

	//Collision Detection Script
	void OnTriggerEnter(Collider other)
	{
		if (other == null)
			return;

		switch (other.tag)
		{
		case "Enemy":
			Debug.Log("This is" + gameObject.tag);
			this.GetComponent<Stats>().currentHP -= other.GetComponent<Stats>().Damage;
			Destroy (other.gameObject);
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			gameController.AddScore (other.GetComponent<Stats>().ScoreValue);
			break;

		case "EnemyWeapon":
			this.GetComponent<Stats>().currentHP -= other.GetComponent<Stats>().Damage;
			Destroy(other.gameObject);
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			break;
		case "PickupHealth":
			if (this.GetComponent<Stats> ().currentHP < this.GetComponent<Stats> ().HP)
			{
				this.GetComponent<Stats> ().currentHP += other.gameObject.GetComponentInParent<scr_HealingPickups> ().HealAmount;
                
                }
                Instantiate(healthPowerup, transform.position, transform.rotation);
                Destroy(other.gameObject);
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
