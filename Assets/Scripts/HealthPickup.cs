using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	public float HealAmount;

	public void RestoreHealth (GameObject objToHeal){
		objToHeal.GetComponent<HealthSystem> ().Heal (HealAmount);
	}
	

}
