using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Dest_Time : MonoBehaviour {

	public float lifetime;

	void Start () 
	{
		Destroy (gameObject, lifetime);
	}
}
