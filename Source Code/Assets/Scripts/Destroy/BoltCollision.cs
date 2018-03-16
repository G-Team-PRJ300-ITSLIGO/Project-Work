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
    public Stats stats;


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
			other.GetComponent<scr_enemyStats>().stats.currentHP -= this.stats.Damage;
			Instantiate (explosion, transform.position, transform.rotation);
                if(this.tag != "SS")
			Destroy(gameObject);
                if(other.GetComponent<scr_enemyStats>().stats.currentHP <= 0)
                {
                    Destroy(other.gameObject);
                    //gameController.HUDcharacter.Play("enemyDown");
                }
			break;
            case "EnemyShield":
                Destroy(gameObject);
                break;
            case "EnemyShieldGenerator":
                other.GetComponent<scr_enemyStats>().stats.currentHP -= this.stats.Damage;
                Instantiate(explosion, transform.position, transform.rotation);
                if (this.tag != "SS")
                    Destroy(gameObject);
                if (other.GetComponent<scr_enemyStats>().stats.currentHP <= 0)
                {
                    gameController.AddScore(other.GetComponent<scr_enemyStats>().stats.ScoreValue);
                }
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
