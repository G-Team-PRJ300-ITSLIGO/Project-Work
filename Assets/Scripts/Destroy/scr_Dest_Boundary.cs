using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Dest_Boundary : MonoBehaviour {
	//Script for destroying anything that leaves the boundary 
	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}
