using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MoverHorizontal : MonoBehaviour {

	private Rigidbody rb;
	public float speedMin;
	public float speedMax;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = -transform.forward * Random.Range(speedMin,speedMax);
	}

}
