using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Dest_Boundary : MonoBehaviour {
	//Script for destroying anything that leaves the boundary 
	public bool playerExit = false;
	void OnTriggerExit(Collider other)
	{
		if(other.tag != "Player")
			Destroy (other.gameObject);
		else
		{
			playerExit = true;
		}
	}
}
