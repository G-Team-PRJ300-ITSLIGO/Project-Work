using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_IgnoreInsideCollision : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary")) 
		{
			return;
		}
	}
}
