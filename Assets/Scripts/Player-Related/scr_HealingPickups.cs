using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_HealingPickups : MonoBehaviour {
	public float HealAmount;

	public void RestoreHealth (GameObject objToHeal){
		objToHeal.GetComponent<scr_HealthSystem> ().Heal (HealAmount);
	}
}
