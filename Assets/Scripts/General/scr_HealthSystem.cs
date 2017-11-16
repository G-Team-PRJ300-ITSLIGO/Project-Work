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

	void Start(){
		//Sets starting Health variable to calculate later on the actual health that player has.
		startHealth = health;
	}

	public void HealthBarUpdate () {
		//Refreshes the health Bar
			if(healthBar != null)
			{
				healthBar.fillAmount = health / startHealth;
			}
	}


	public void TakeDamage(GameObject obj, float amount){
		obj.GetComponent<scr_HealthSystem>().health -= amount;
			if(obj.GetComponent<scr_HealthSystem>().healthBar != null)
			{
				obj.GetComponent<scr_HealthSystem>().healthBar.fillAmount = health / startHealth;
			}	
	}


	public void Heal(float amount){
		this.health += amount;

		if (health > startHealth) {
			health = startHealth;
		}
			if(healthBar != null)
			{
				healthBar.fillAmount = health / startHealth;
			}	
	}
}