using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_HealthSystem : MonoBehaviour {
	
	//Amount of 'Health' that object will have.
	public float health;
	//Var used to calculate the actual health for displaying it.
	private float startHealth;
	//Image that will visually display the amount of health object has.
	public Image healthBar;
	private scr_playerBehaviour player;

	void Start(){
        //Sets starting Health variable to calculate later on the actual health that player has.
        FindPlayer();
		startHealth = player.GetComponent<Stats>().currentHP;
	}

	public void Update () {
		//Refreshes the health Bar
			if(healthBar != null && player != null)
			{
			if (player.GetComponent<Stats>().currentHP > startHealth) player.GetComponent<Stats>().currentHP = startHealth;
			healthBar.fillAmount = player.GetComponent<Stats>().currentHP / startHealth;
			}
	}

    void FindPlayer()
    {
        GameObject playerControllerObj = GameObject.FindWithTag("Player");
        if (playerControllerObj != null)
        {
            player = playerControllerObj.GetComponent<scr_playerBehaviour>();
        }
        if (player == null)
        {
            Debug.Log("Cannot Find 'PlayerController' Script");
        }
    }
}